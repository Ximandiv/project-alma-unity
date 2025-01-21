using Scripts.Player;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int damage = 2; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Llamar al m�todo TakeDamage y pasar el da�o configurado
                playerHealth.Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}
