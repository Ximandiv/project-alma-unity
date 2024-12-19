using UnityEngine;

public class PlayerAttackTest : MonoBehaviour
{
    public int attackDamage = 2;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    public string colorLuz;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;
        Color color = spriteRenderer.color;
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
                Debug.Log("Dano esferas");
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
        if (spriteRenderer.color == Color.yellow)
        {
            spriteRenderer.color = Color.red;
            colorLuz = "Rojo";
        }
        else if (spriteRenderer.color == Color.red)
        {
            spriteRenderer.color = Color.blue;
            colorLuz = "Azul";
        }
        else if (spriteRenderer.color == Color.blue)
        {
            spriteRenderer.color = Color.yellow;
            colorLuz = "Amarillo";
        }

        Color color = spriteRenderer.color;
        color.a = (0.5f);
    }
}
