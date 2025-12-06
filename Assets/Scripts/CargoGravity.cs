using UnityEngine;

public class CargoGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private ShipController shipController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        shipController = FindAnyObjectByType<ShipController>();
    }

    void Update()
    {
        if (rb.gravityScale == 0 && shipController != null && shipController.gameStarted)
        {
            ActivateGravity();
        }
    }

    public void ActivateGravity()
    {
        rb.gravityScale = 1;
    }
}