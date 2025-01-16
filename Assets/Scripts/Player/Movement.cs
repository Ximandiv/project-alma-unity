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

        public delegate void OnMovement(bool isMoving);
        public event OnMovement onMoving;
        public delegate void OnFlip(bool isRotating);
        public event OnFlip onFlip;

        [SerializeField] private CharacterStatus status;
        [SerializeField] private CharacterSpeed speed;
        [SerializeField] private GameStatus gameStatus;
        [SerializeField] private Rigidbody2D rb;

        private Vector2 movement = Vector2.zero;
        private float minMovementValue = 0.01f;

        #endregion

        public void Initialize(
            CharacterStatus newStatus,
            CharacterSpeed newSpeed,
            GameStatus newGameStatus,
            Rigidbody2D newRb)
        {
            status = newStatus;
            speed = newSpeed;
            gameStatus = newGameStatus;
            rb = newRb;
        }

        #region Unity API Methods

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

            if(isMoving())
                onMoving?.Invoke(true);
            else
                onMoving?.Invoke(false);
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

            if (normalizedMovement.x < 0)
                onFlip?.Invoke(true);
            else if(normalizedMovement.x > 0)
                onFlip?.Invoke(false);
        }

        private bool isMoving() => (movement.normalized).sqrMagnitude > minMovementValue;

        private bool canMove() => status.IsCapableOfMovement() && !gameStatus.IsPaused;

        #endregion
    }
}
