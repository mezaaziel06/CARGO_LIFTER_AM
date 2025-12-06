
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [Header("UI de Victoria")]
    public GameObject winPanel;

    [Header("Referencias de la Nave y Carga")]
    public ShipController shipController;
    public Rigidbody2D shipRb;
    public Rigidbody2D cargoRb;

    private bool victoryTriggered = false;

    public async void TriggerVictory()
    {
        if (victoryTriggered) return;
        victoryTriggered = true;

        Debug.Log("Victoria completa.");

        // Activar UI
        if (winPanel != null)
            winPanel.SetActive(true);

        // Bloquear controles
        if (shipController != null)
            shipController.enabled = false;

        // Detener f�sica
        shipRb.linearVelocity = Vector2.zero;
        cargoRb.linearVelocity = Vector2.zero;

        shipRb.gravityScale = 0;
        cargoRb.gravityScale = 0;

        // Esperar (Awaitable)
        await Awaitable.WaitForSecondsAsync(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
