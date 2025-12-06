using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    private HashSet<string> objectsInGoal = new HashSet<string>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cargo"))
        {
            objectsInGoal.Add(other.tag);
            CheckWinCondition();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cargo"))
        {
            objectsInGoal.Remove(other.tag);
        }
    }

    private void CheckWinCondition()
    {
        bool hasPlayer = objectsInGoal.Contains("Player");
        bool hasCargo = objectsInGoal.Contains("Cargo");

        if (hasPlayer && hasCargo)
        {
            Debug.Log("¡VICTORIA! Nave y Carga llegaron a la meta.");
            EndGameSuccess();
        }
    }

    private void EndGameSuccess()
    {
        // Pausa el juego
        Time.timeScale = 0f;
    }
}