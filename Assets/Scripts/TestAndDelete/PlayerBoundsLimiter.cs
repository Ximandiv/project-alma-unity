using UnityEngine;

public class PlayerBoundsLimiter : MonoBehaviour
{
    public Transform player;           // Referencia al jugador
    public Vector2 minBounds;          // Límite mínimo (x, y)
    public Vector2 maxBounds;          // Límite máximo (x, y)

    private void Update()
    {
        // Limitar la posición del jugador dentro de los límites
        float clampedX = Mathf.Clamp(player.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(player.position.y, minBounds.y, maxBounds.y);

        player.position = new Vector3(clampedX, clampedY, player.position.z);
    }
}
