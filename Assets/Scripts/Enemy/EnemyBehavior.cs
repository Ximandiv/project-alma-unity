using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        #region Private Variables

        // Enemy Variables
        [Header("Configuration Variables")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int damageToPlayer = 1;
        [SerializeField] private float attackRange = 10f;
        [SerializeField] private string color;
        [SerializeField] private float distanceToApproach = 5f;
        [SerializeField] private float circleDuration = 5f;
        [SerializeField] private float circleRadius = 2f;

        // Particles system 
        [Header("Particle System")]
        [SerializeField] private GameObject deathParticles;
        [SerializeField] private GameObject deathParticles2;

        // Enemy life
        private int currentHealth;

        // References
        private Transform player;
        private Rigidbody2D rb;
        private Vector2 movement;
        private Animator animator;

        // Enemy status
        private bool isFacingRight = false;
        private bool isAttacking = false;
        private bool isCircling = false;

        #endregion

        void Start()
        {
            currentHealth = maxHealth;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("No se encontró un objeto con la etiqueta 'Player'");
            }

            StartCoroutine(ApproachAndCirclePlayer());
        }

        void Update()
        {
            if (player != null && !isAttacking)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                movement = direction;

                // Movement Animation
                animator.SetBool("isMoving", movement.magnitude > 0);

                // Check if the player is in the range
                float distanceToPlayer = Vector2.Distance(player.position, transform.position);
                if (distanceToPlayer <= attackRange)
                {
                    StartCoroutine(AttackPlayer());
                }

                // Flip the sprite depending on the player's position 
                HandleSpriteFlip();
            }
            else
            {
                // If is attacking stop the movement al play animation
                movement = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
        }

        void FixedUpdate()
        {
            // Move the enemy
            if (!isAttacking && player != null)
            {
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            }
        }

        IEnumerator AttackPlayer()
        {
            isAttacking = true;
            animator.SetTrigger("attack");

            // Here it assumes that the player has a script called "PlayerHealth"
            PlayerHealthTest playerHealth = player.GetComponent<PlayerHealthTest>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }

            yield return new WaitForSeconds(1f);

            // Back to normal status
            isAttacking = false;
        }

        public void TakeDamage(int damage, string colorDano)
        {
            if (color == colorDano)
            {
                // Take damage
                currentHealth -= damage;

                // Plays animation of damage
                //animator.SetTrigger("Hurt");

                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }

        void Die()
        {
            //animator.SetTrigger("Die");

            // Particle system effect
            if (deathParticles != null)
            {
                Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                GameObject particles = Instantiate(deathParticles, transform.position, rotation);

                // Destroy the particle after played
                float particleDuration = particles.GetComponent<ParticleSystem>().main.duration;
                Destroy(particles, particleDuration);

                GameObject particles2 = Instantiate(deathParticles2, transform.position, Quaternion.identity);

                // Destroy the particle after played
                float particleDuration2 = particles2.GetComponent<ParticleSystem>().main.duration;
                Destroy(particles2, particleDuration2);
            }

            Destroy(gameObject);
        }

        void HandleSpriteFlip()
        {
            // Check if the player is right or left
            if (player != null)
            {
                bool shouldFaceRight = player.position.x > transform.position.x;

                // Change direction
                if (shouldFaceRight && !isFacingRight)
                {
                    Flip();
                }
                else if (!shouldFaceRight && isFacingRight)
                {
                    Flip();
                }
            }
        }

        void Flip()
        {
            // Change direction
            isFacingRight = !isFacingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        IEnumerator ApproachAndCirclePlayer()
        {
            // step 1: close in on the player to the set distance
            while (Vector2.Distance(transform.position, player.position) > distanceToApproach)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                movement = direction;

                yield return null;
            }

            // Once it's close enough, start the circle
            isCircling = true;
            StartCoroutine(CircleAroundPlayer());

            yield return new WaitForSeconds(circleDuration);

            // Stop walking in circles and start attacking
            isCircling = false;
            //isAttacking = true;
        }

        IEnumerator CircleAroundPlayer()
        {
            float elapsedTime = 0f;

            // While walking in circles, move it around the player
            while (isCircling)
            {
                elapsedTime += Time.deltaTime;

                float angle = elapsedTime * moveSpeed;
                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * circleRadius;
                movement = (player.position + (Vector3)offset - transform.position).normalized;

                yield return null;
            }
        }
    }
}
