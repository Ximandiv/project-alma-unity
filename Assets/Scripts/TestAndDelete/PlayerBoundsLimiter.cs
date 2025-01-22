using UnityEngine;

public class PlayerBoundsLimiter : MonoBehaviour
{
    public Transform player;           // Referencia al jugador
    public Vector2 minBounds;          // L�mite m�nimo (x, y)
    public Vector2 maxBounds;          // L�mite m�ximo (x, y)

    private void Update()
    {
        // Limitar la posici�n del jugador dentro de los l�mites
        float clampedX = Mathf.Clamp(player.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(player.position.y, minBounds.y, maxBounds.y);

        player.position = new Vector3(clampedX, clampedY, player.position.z);
    }
}
