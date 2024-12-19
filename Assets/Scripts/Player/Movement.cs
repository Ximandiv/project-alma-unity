using Scripts.Common;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(CharacterVariablesConfig))]
    [RequireComponent(typeof(CharacterSpeed))]
    [RequireComponent(typeof(CharacterStatus))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private CharacterVariablesConfig characterVariables;
        [SerializeField] private CharacterSpeed characterSpeed;
        [SerializeField] private CharacterStatus characterStatus;
        [SerializeField] private Rigidbody2D rb;

        private Vector2 movement = Vector2.zero;
        private Vector2 lastDirection;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            characterVariables = GetComponent<CharacterVariablesConfig>();
            characterSpeed = GetComponent<CharacterSpeed>();
            characterStatus = GetComponent<CharacterStatus>();
            
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            int maxHitpoints = 10;
            int currHitPoints = 10;
            float maxSpeed = 12f;
            float currSpeed = 8f;

            characterVariables.ConfigureVariables(maxSpeed,
                                                    currSpeed,
                                                    maxHitpoints,
                                                    currHitPoints);
        }

        private void Update()
        {
            if (characterStatus.IsCapableOfMovement())
            {
                getInputMovement();

                characterStatus.SetMovingStatus(isMoving());
            }
            else
                characterStatus.SetMovingStatus(false);
        }

        private void FixedUpdate()
        {
            if (characterStatus.IsCapableOfMovement())
                move();
        }

        #endregion

        #region Private Methods

        private bool isMoving() => (movement.normalized).sqrMagnitude > 0.01f;

        private void getInputMovement()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        private void move()
        {
            Vector2 normalizedMovement = movement.normalized;

            rb.linearVelocity = normalizedMovement * characterSpeed.GetCurrentSpeed();

            if (isMoving())
            {
                lastDirection = normalizedMovement;
                rotate();
            }
        }

        private void rotate()
        {
            float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;

            rb.rotation = angle;
        }

        #endregion
    }
}
