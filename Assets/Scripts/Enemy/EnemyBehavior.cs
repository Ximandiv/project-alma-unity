using Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [Header("Configuration")]
        public float moveSpeed = 3f;
        public int maxHealth = 10;
        public int damageToPlayer = -1;
        public float attackRange = 10f;
        public string color;
        public float distanceToApproach = 5f;
        public float circleDuration = 5f;
        public float circleRadius = 2f;

        [Header("VFX Particles")]
        [SerializeField] private GameObject deathParticles;
        [SerializeField] private GameObject deathParticles2;

        [Header("PowerUps Prefabs")]
        public List<GameObject> powerUps;

        [Header("Audio Clips")]
        public AudioClip deadClip;
        public AudioClip attackClip;
        public float sfxVolumen;

        // References
        private Transform player;
        private Rigidbody2D rb;
        private Vector2 movement;
        private Animator animator;

        // Enemy status
        private bool isFacingRight = false;
        private bool isAttacking = false;
        private bool isCircling = false;

        private int currentHealth;

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
                Debug.LogError("No se encontr� un objeto con la etiqueta 'Player'");
            }

            StartCoroutine(ApproachAndCirclePlayer());
        }

        void Update()
        {
            if (player != null && !isAttacking)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                movement = direction;

                animator.SetBool("isMoving", movement.magnitude > 0);

                float distanceToPlayer = Vector2.Distance(player.position, transform.position);
                if (distanceToPlayer <= attackRange)
                {
                    StartCoroutine(AttackPlayer());
                }

                HandleSpriteFlip();
            }
            else
            {
                movement = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
        }

        void FixedUpdate()
        {
            if (!isAttacking && player != null)
            {
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            }
        }

        IEnumerator AttackPlayer()
        {
            AudioManager.Instance.PlaySFX(attackClip, sfxVolumen);

            isAttacking = true;

            animator.SetTrigger("attack");

            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damageToPlayer);
            }

            yield return new WaitForSeconds(1f);

            isAttacking = false;
        }

        public void TakeDamage(int damage, string colorDano)
        {
            if (color == colorDano)
            {
                currentHealth -= damage;

                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }

        void Die()
        {
            AudioManager.Instance.PlaySFX(deadClip, sfxVolumen);

            if (deathParticles != null)
            {
                Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                GameObject particles = Instantiate(deathParticles, transform.position, rotation);

                float particleDuration = particles.GetComponent<ParticleSystem>().main.duration;
                Destroy(particles, particleDuration);

                GameObject particles2 = Instantiate(deathParticles2, transform.position, Quaternion.identity);

                float particleDuration2 = particles2.GetComponent<ParticleSystem>().main.duration;
                Destroy(particles2, particleDuration2);
            }

            SpawnRandomPower(transform.position);

            Destroy(gameObject);
        }

        void HandleSpriteFlip()
        {
            if (player != null)
            {
                bool shouldFaceRight = player.position.x > transform.position.x;

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
            isFacingRight = !isFacingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        IEnumerator ApproachAndCirclePlayer()
        {
            while (Vector2.Distance(transform.position, player.position) > distanceToApproach)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                movement = direction;

                yield return null;
            }

            isCircling = true;
            StartCoroutine(CircleAroundPlayer());

            yield return new WaitForSeconds(circleDuration);

            isCircling = false;
        }

        IEnumerator CircleAroundPlayer()
        {
            float elapsedTime = 0f;

            while (isCircling)
            {
                elapsedTime += Time.deltaTime;

                float angle = elapsedTime * moveSpeed;
                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * circleRadius;
                movement = (player.position + (Vector3)offset - transform.position).normalized;

                yield return null;
            }
        }

        public void SpawnRandomPower(Vector2 position)
        {
            int randomValue = Random.Range(0, 10);

            if (randomValue < 1)
            {
                if (powerUps == null || powerUps.Count == 0)
                {
                    return;
                }

                int randomIndex = Random.Range(0, powerUps.Count);
                GameObject selectedPrefab = powerUps[randomIndex];

                Instantiate(selectedPrefab, position, Quaternion.identity);
            }
        }
    }
}
