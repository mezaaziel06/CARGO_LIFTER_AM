using UnityEngine;

public class GoalZone : MonoBehaviour
{
    public Transform ship;
    public Transform cargo;
    public VictoryManager victoryManager;   // ← AGREGAMOS ESTO

    private bool shipInside = false;
    private bool cargoInside = false;
    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (levelCompleted) return;

        if (other.transform == ship)
            shipInside = true;

        if (other.transform == cargo)
            cargoInside = true;

        if (shipInside && cargoInside)
        {
            levelCompleted = true;
            Debug.Log("¡Victoria detectada!");

            // LLAMA A VICTORYMANAGER
            victoryManager.TriggerVictory();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == ship)
            shipInside = false;

        if (other.transform == cargo)
            cargoInside = false;
    }
}
