using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int damage = 2; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Intentar obtener el componente del jugador que tiene el m�todo TakeDamage
            //PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    // Llamar al m�todo TakeDamage y pasar el da�o configurado
            //    playerHealth.TakeDamage(damage);
            //}

            Destroy(gameObject);
        }
    }
}
