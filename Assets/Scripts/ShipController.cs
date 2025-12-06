using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [Header("Fuerzas")]
    public float thrustForce = 10f;
    public float lateralForce = 5f;

    [Header("Propulsor")]
    public Renderer thrusterRenderer;
    public Color activeColor = Color.yellow;

    private Color originalColor;
    private Rigidbody2D rb;

    private bool isThrusting = false;
    private float steerValue = 0f;
    public bool gameStarted { get; private set; } = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalColor = thrusterRenderer.material.color;

        // RF-01: estado inicial sin gravedad
        rb.gravityScale = 0;
    }

    // ------------------------------
    // EVENTO: THRUST (Espacio)
    // ------------------------------
    public void OnThrust(InputAction.CallbackContext context)
    {
        // Al presionar por primera vez, activar gravedad
        if (context.started && !gameStarted)
        {
            rb.gravityScale = 1;
            gameStarted = true;
        }

        if (context.performed)
        {
            // Estamos presionando (activar propulsión)
            isThrusting = true;
            thrusterRenderer.material.color = activeColor;
        }
        else if (context.canceled)
        {
            // Se soltó el botón
            isThrusting = false;
            thrusterRenderer.material.color = originalColor;
        }
    }

    // ------------------------------
    // EVENTO: STEER (A/D o Flechas)
    // ------------------------------
    public void OnSteer(InputAction.CallbackContext context)
    {
        steerValue = context.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        if (isThrusting)
        {
            rb.linearDamping = 0.5f;
            rb.AddForce(Vector2.up * thrustForce, ForceMode2D.Force);
        }
        else
        {
            rb.linearDamping = 2f;
        }


        if (steerValue != 0)
        {
            rb.AddForce(Vector2.right * steerValue * lateralForce, ForceMode2D.Force);
        }
    }


}
