using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour
{
    public float moveSpeed = 2f;            // Boss speed
    public float closeAttackRange = 3f;     // Range attack 1
    public float farAttackRange = 10f;      // Range attack 2
    public float idleTime = 2f;             // Time to idle after attack
    public GameObject projectilePrefab;     // Proyectile prefab
    public float projectileSpeed = 5f;      // Proyectile speed
    public float projectileDistance = 10f;  // Proyectile max distance


    // Cooldown variables for attack
    public float closeAttackCooldown = 5f; // Cooldown attack 1
    public float farAttackCooldown = 10f;  // Cooldown attack 2

    public int maxHealth = 100;
    private int currentHealth;

    private bool isInvulnerable = true;     // Sphere-based invulnerability

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isIdle = false;
    private bool isPerformingAttack = false;

    private float nextCloseAttackTime = 0f; // Time to next Attack 1
    private float nextFarAttackTime = 0f;   // Time to next Attack 2

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Search the player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontro un objeto con la etiqueta 'Player'");
        }

        //CreateSpheres();
    }

    void Update()
    {
        if (player == null || isIdle || isPerformingAttack) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= closeAttackRange && Time.time >= nextCloseAttackTime)
        {
            StartCoroutine(CloseAttack());
        }
        else if (distanceToPlayer <= farAttackRange && Time.time >= nextFarAttackTime)
        {
            StartCoroutine(FarAttack());
        }
        else
        {
            ChasePlayer();
        }

        //UpdateSpheresOrbit();
    }

    void ChasePlayer()
    {
        // Moving towards the player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        animator.SetBool("walk", true);
    }

    IEnumerator CloseAttack()
    {
        isPerformingAttack = true;

        nextCloseAttackTime = Time.time + closeAttackCooldown;
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("walk", false);
        isIdle = true;
        yield return new WaitForSeconds(idleTime);

        isIdle = false;
        isPerformingAttack = false;
    }

    IEnumerator FarAttack()
    {
        isPerformingAttack = true;

        nextFarAttackTime = Time.time + farAttackCooldown;

        animator.SetTrigger("attack02");

        // Spawn 10 proyectiles
        for (int i = 0; i < 10; i++)
        {
            float angle = i * (360f / 10);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 direction = rotation * Vector3.up;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation);
            Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
            projRb.linearVelocity = direction * projectileSpeed;

            Destroy(projectile, projectileDistance / projectileSpeed);
        }

        yield return new WaitForSeconds(1f);

        isPerformingAttack = false;
    }

    /* void UpdateSpheresOrbit()
     {
         if (spheres == null) return;

         for (int i = 0; i < spheres.Length; i++)
         {
             float angle = Time.time * sphereOrbitSpeed + (360f / spheres.Length) * i;
             Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * sphereOrbitDistance;
             if (spheres[i] != null)
             {
                 spheres[i].transform.position = transform.position + offset;
             }
         }

         // Actualizar invulnerabilidad
         isInvulnerable = AreAllSpheresActive();
     }

     bool AreAllSpheresActive()
     {
         foreach (GameObject sphere in spheres)
         {
             if (sphere == null) return false; // Una esfera destruida hace al jefe vulnerable
         }
         return true;
     }*/

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject);
    }
}
