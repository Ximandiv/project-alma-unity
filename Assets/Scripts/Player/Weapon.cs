using Scripts.Scriptables;
using UnityEngine;
using Scripts.Enemy;
using UnityEngine.Rendering.Universal;

namespace Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private GameStatus gameStatus;

        [Header("Layer Masks")]

        [SerializeField] private LayerMask enemyLayer;

        [Header("Transform Components")]

        [SerializeField] private Transform player;

        /* 
         * The pragma warnings ignore using the 'new' keyword on Light due to it being a Light2D
         * wanting to hide the inhereted members and use only transform. We ignore it since
         * it's not necessary
         */
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        [SerializeField] private Transform light;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        [Header("Light Direction")]

        /* 
         * Since object is flipped out to simulate a flashlight,
         * this is used to adjust the physics overlap and light2d processes
         * feel free to remove and rework when flashlight asset exists
        */
        [SerializeField] private Vector3 lightDownwardDir;

        [Header("Weapon Orbit")]

        // Uses an orbit to tell where the weapon can move around in x radius of the player
        [SerializeField] private float orbitRadius = 1.25f;

        [Header("Detection Distance and Angle")]

        [SerializeField] private float detectionDistance = 5f;

        [SerializeField] private float coneAngle = 45f;
        [SerializeField] private float rotationAngle;

        [SerializeField] private string currentColor = "red";
        [SerializeField] private int attackDamage = 2;

        private int coneAngleOffset = 2;
        private int rotationAngleOffset = 90;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            light = GameObject.FindGameObjectWithTag("WeaponLight").transform;
            light.GetComponent<Light2D>().color = Color.red;
        }

        private void Update()
        {
            if (gameStatus.IsPaused) return;

            if(Input.GetMouseButtonDown(0))
            {
                var lightComponent = light.GetComponent<Light2D>();
                switch (currentColor)
                {
                    case "red":
                        currentColor = "blue";
                        lightComponent.color = Color.blue;
                        break;
                    case "blue":
                        currentColor = "purple";
                        lightComponent.color = new Color(0.5f, 0f, 0.5f);
                        break;
                    case "purple":
                    default:
                        currentColor = "red";
                        lightComponent.color = Color.red;
                        break;
                }
            }

            moveAroundPlayerByMousePos(out Vector3 direction);
            rotateToMouseDirection(direction);

            lightDownwardDir = -light.up;
            detectEnemiesOnLight();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(light.position, detectionDistance);

            Vector3 leftBoundary = Quaternion.Euler(0, 0, -coneAngle / 2) * lightDownwardDir * detectionDistance;
            Vector3 rightBoundary = Quaternion.Euler(0, 0, coneAngle / 2) * lightDownwardDir * detectionDistance;

            Gizmos.DrawLine(light.position, light.position + leftBoundary);
            Gizmos.DrawLine(light.position, light.position + rightBoundary);
        }

        #endregion

        #region Private Methods

        private void moveAroundPlayerByMousePos(out Vector3 direction)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            direction = (mousePos - player.position).normalized;

            // We use local because we want to affect only the sprite relative to parent
            transform.localPosition = player.position + direction * orbitRadius;
        }

        private void rotateToMouseDirection(Vector3 direction)
        {
            rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // We use local because we want to affect only the sprite relative to parent
            transform.localRotation = Quaternion.Euler(0, 0, rotationAngle + rotationAngleOffset);
        }

        private void detectEnemiesOnLight()
        {
            if(gameStatus.IsPaused) return;

            Collider2D[] colliders = getEnemiesWithinRange();

            if(areThereEnemiesInRange(colliders))
                damageEnemies(colliders);
        }

        private void damageEnemies(Collider2D[] enemyColliderList)
        {
            foreach (Collider2D enemyCollider in enemyColliderList)
            {
                Vector3 directionToEnemy = (enemyCollider.transform.position - light.position).normalized;
                float angleToEnemy = Vector3.Angle(lightDownwardDir, directionToEnemy);

                if (isEnemyWithinRange(angleToEnemy))
                {
                    EnemyBehavior enemyBehavior = enemyCollider.GetComponent<EnemyBehavior>();
                    if (enemyBehavior is not null)
                        enemyBehavior.TakeDamage(attackDamage, currentColor);

                    BossBehavior bossBehavior = enemyCollider.GetComponent<BossBehavior>();
                    if (bossBehavior is not null)
                        bossBehavior.TakeDamage(attackDamage);

                    WeakPoint weakPoint = enemyCollider.GetComponent<WeakPoint>();
                    if (weakPoint is not null)
                        weakPoint.TakeDamage(currentColor);
                }
            }
        }

        private Collider2D[] getEnemiesWithinRange() => Physics2D.OverlapCircleAll(light.position, detectionDistance, enemyLayer);

        private bool isEnemyWithinRange(float angleToEnemy) => angleToEnemy < coneAngle / coneAngleOffset;

        private bool areThereEnemiesInRange(Collider2D[] colliders) => colliders.Length > 0;

        #endregion
    }
}