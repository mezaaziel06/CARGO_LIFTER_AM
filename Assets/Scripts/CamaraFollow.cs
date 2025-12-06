using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // La nave
    public float smoothTime = 0.25f;  // Suavidad del movimiento

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Posición objetivo (misma posición de la nave pero manteniendo Z)
        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        // Movimiento suave
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }
}
