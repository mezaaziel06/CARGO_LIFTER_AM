using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Unity.Awaitable;

public class ShipController : MonoBehaviour
{
    // === PARÁMETROS EN INSPECTOR ===
    public float thrustForce;
    public float lateralForce;
    public Renderer thrusterRenderer;
    public Color activeColor;

    // === Variables Internas ===
    private Rigidbody2D rb;
    private Color originalColor;
    private bool isThrusting = false;
    private float steerInput = 0f;
    private bool isGameOver = false;

    private const float DefaultDrag = 0f;
    private const float SoftFallDrag = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Estado inicial: sin gravedad
        rb.gravityScale = 0f;
        rb.drag = DefaultDrag;

        if (thrusterRenderer != null)
            originalColor = thrusterRenderer.material.color;

        // Congelar rotación
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // === INPUT ===
    public void OnThrust(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Activar gravedad en el primer input
            if (rb.gravityScale == 0f)
                rb.gravityScale = 1f;

            isThrusting = true;

            // Feedback visual
            if (thrusterRenderer != null)
                thrusterRenderer.material.color = activeColor;

            rb.drag = DefaultDrag;
        }
        else if (context.canceled)
        {
            isThrusting = false;

            if (thrusterRenderer != null)
                thrusterRenderer.material.color = originalColor;

            // Caída suave
            rb.drag = SoftFallDrag;
        }
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        steerInput = context.ReadValue<float>();
    }

    void FixedUpdate()
    {
        if (isThrusting && rb.gravityScale > 0f)
            rb.AddForce(Vector2.up * thrustForce, ForceMode2D.Force);

        if (steerInput != 0f)
            rb.AddForce(Vector2.right * steerInput * lateralForce, ForceMode2D.Force);
    }

    // === Reinicio Asíncrono ===
    public async void HandleDefeat()
    {
        if (isGameOver) return;
        isGameOver = true;

        await Task.Delay(2000); // 2 segundos reales

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
