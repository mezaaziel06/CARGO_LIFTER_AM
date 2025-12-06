
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargoMonitor : MonoBehaviour
{
    public Transform ship; // La nave
    public float fallLimit = -20f; // Límite inferior para detectar caída
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (HasFallen())
        {
            RestartLevel();
        }

        if (IsOutOfCamera())
        {
            RestartLevel();
        }
    }

    private bool HasFallen()
    {
        return transform.position.y < fallLimit;
    }

    private bool IsOutOfCamera()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);

        // Si está completamente fuera de la pantalla → derrota
        return viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1;
    }

    private async void RestartLevel()
    {
        Debug.Log("Derrota: La carga cayó o salió de cámara");

        // Espera obligatoria usando Awaitable (NO corutinas)
        await Awaitable.WaitForSecondsAsync(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
