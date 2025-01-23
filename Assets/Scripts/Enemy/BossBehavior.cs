using UnityEngine;
using System.Collections;
using Scripts.Player;
using Scripts.Events;

public class BossBehavior : MonoBehaviour
{
    [Header("Control variables")]
    public float moveSpeed = 4f;            
    public float closeAttackRange = 3f;    
    public float farAttackRange = 10f;      
    public float idleTime = 2f;             

    [Header("Projectile variables")]
    public GameObject projectilePrefab;    
    public float projectileSpeed = 5f;      
    public float projectileDistance = 10f; 

    [Header("CoolDown time")]
    public float closeAttackCooldown = 5f;  
    public float farAttackCooldown = 10f;  

    [Header("Boss Health")]
    public int maxHealth = 100;             
    public int currentHealth;

    [Header("Behavior variable")]
    public bool isInvulnerable = true;     
    public bool startAttack = false;       

    [Header("WeakPoints variables")]
    public Transform[] spheres;         
    public float orbitSpeed = 50f;      
    public float cooldownSphere = 5f;   

    [Header("Attack damage variables")]
    public LayerMask playerLayer;      
    public float attackRange = 1.5f;  
    public int CloseAttackDamage = -2;  

    [Header("Audio Clips")]
    public AudioClip deadClip;
    public AudioClip attack1Clip;
    public AudioClip attack2Clip;
    public float sfxVolumen;

    [Header("Particle System")]
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject deathParticles2;

    // Reference
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private ParticleEffectWinner particleEffectWinner;

    private bool isIdle = false;             
    private bool isPerformingAttack = false; 
    private bool isTakingDamage = false;     

    private float nextCloseAttackTime = 0f; 
    private float nextFarAttackTime = 0f;   

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        particleEffectWinner = GetComponent<ParticleEffectWinner>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontro un objeto con la etiqueta 'Player'");
        }
    }

    void Update()
    {
        if (!startAttack) return;

        foreach (Transform sphere in spheres)
        {
            sphere.RotateAround(transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        }

        UpdateSpheresOrbit();

        if (player == null || isIdle || isPerformingAttack || isTakingDamage) return;

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
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        animator.SetBool("walk", true);
    }

    IEnumerator CloseAttack()
    {
        animator.SetBool("walk", false);
        AudioManager.Instance.PlaySFX(attack1Clip, sfxVolumen);

        isPerformingAttack = true;

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.Damage(CloseAttackDamage);
            }
        }

        nextCloseAttackTime = Time.time + closeAttackCooldown;

        animator.SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);

        isIdle = true;
        yield return new WaitForSeconds(idleTime);

        isIdle = false;
        isPerformingAttack = false;
    }

    IEnumerator FarAttack()
    {
        animator.SetBool("walk", false);
        AudioManager.Instance.PlaySFX(attack2Clip, sfxVolumen);

        isPerformingAttack = true;

        nextFarAttackTime = Time.time + farAttackCooldown;

        animator.SetTrigger("attack02");

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

    void UpdateSpheresOrbit()
    {
        if (spheres == null) return;
        isInvulnerable = AreAllSpheresActive();
    }

    bool AreAllSpheresActive()
    {
        int sphereActive;
        sphereActive = 0;

        foreach (Transform sphere in spheres)
        {
            if (!sphere.gameObject.activeSelf)
                sphereActive++;
        }

        if (sphereActive == 3) return false;

        return true;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;
        if (!startAttack) return;

        currentHealth -= damage;

        animator.SetBool("walk", false);
        animator.SetTrigger("damage");
        isTakingDamage = true;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
        StartCoroutine(ActivateAfterCooldown());
    }

    IEnumerator Die()
    {
        AudioManager.Instance.PlaySFX(deadClip, sfxVolumen);
        animator.SetTrigger("damage");

        yield return new WaitForSeconds(1f);

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

        particleEffectWinner.SpawnWinnerParticle(player.transform.position);

        GameEvents.Instance.LevelBeaten();

        Destroy(gameObject);
    }

    IEnumerator ActivateAfterCooldown()
    {
        yield return new WaitForSeconds(cooldownSphere);

        foreach (Transform sphere in spheres)
        {
            sphere.gameObject.SetActive(true);
        }
        isTakingDamage = false;
    }
}
