
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargoMonitor : MonoBehaviour
{
    public float fallLimit = -5f;
    private bool isRestarting = false;

    private void Update()
    {
        if (!isRestarting && transform.position.y < fallLimit)
        {
            RestartLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta suelo por nombre del objeto
        if (collision.collider.gameObject.name == "Suelo")
        {
            RestartLevel();
        }
    }

    private async void RestartLevel()
    {
        if (isRestarting) return;
        isRestarting = true;

        Debug.Log("DERROTA: La carga cayó");

        await Awaitable.WaitForSecondsAsync(1.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
