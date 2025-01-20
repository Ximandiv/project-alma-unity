using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAttackTest : MonoBehaviour
{
    public int attackDamage = 2; 
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    public string colorLuz;

    public float cooldownSphere = 5.0f;

    private SpriteRenderer spriteRenderer;
    private Light2D light2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();
        light2D.color = Color.yellow;
        Color color = light2D.color;
        colorLuz = "Amarillo";
        color.a = (0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeColor();
        }

        Attack();
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Scripts.Enemy.EnemyBehavior enemyBehavior = enemy.GetComponent<Scripts.Enemy.EnemyBehavior>();
            if (enemyBehavior != null)
            {
                enemyBehavior.TakeDamage(attackDamage, colorLuz);
            }

            BossBehavior bossBehavior = enemy.GetComponent<BossBehavior>();
            if (bossBehavior != null)
            {
                bossBehavior.TakeDamage(attackDamage);
            }

            WeakPoint weakPoint = enemy.GetComponent<WeakPoint>();
            if (weakPoint != null)
            {
                weakPoint.TakeDamage(colorLuz);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void ChangeColor()
    {
        if (light2D.color == Color.yellow)
        {
            light2D.color = Color.red;
            colorLuz = "Rojo";
        }
        else if (light2D.color == Color.red)
        {
            light2D.color = Color.blue;
            colorLuz = "Azul";
        }
        else if (light2D.color == Color.blue)
        {
            light2D.color = Color.yellow;
            colorLuz = "Amarillo";
        }

        Color color = light2D.color;
        color.a = (0.5f);
    }
}
