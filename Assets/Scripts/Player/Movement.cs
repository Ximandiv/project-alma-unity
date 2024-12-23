using Scripts.Common;
using Scripts.Scriptables;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(CharacterStatus))]
    [RequireComponent(typeof(CharacterSpeed))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private CharacterStatus status;
        [SerializeField] private GameStatus gameStatus;
        [SerializeField] private CharacterSpeed speed;
        [SerializeField] private Rigidbody2D rb;

        private Vector2 movement = Vector2.zero;
        private Vector2 lastDirection;
        private float minMovementValue = 0.01f;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            status = GetComponent<CharacterStatus>();
            speed = GetComponent<CharacterSpeed>();
            
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if ( canMove() )
                getInputMovement();

            status.SetMovingStatus( isMoving() );
        }

        private void FixedUpdate()
        {
            if ( canMove() )
                move();
            else
                rb.linearVelocity = Vector2.zero;
        }

        #endregion

        #region Private Methods

        private void getInputMovement()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        private void move()
        {
            Vector2 normalizedMovement = movement.normalized;

            rb.linearVelocity = normalizedMovement * speed.GetCurrentSpeed();

            if ( isMoving() )
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

        private bool isMoving() => (movement.normalized).sqrMagnitude > minMovementValue;

        private bool canMove() => status.IsCapableOfMovement() || !gameStatus.IsPaused;

        #endregion
    }
}
