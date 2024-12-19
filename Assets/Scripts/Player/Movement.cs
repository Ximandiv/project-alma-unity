using Scripts.Common;
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
        [SerializeField] private CharacterSpeed speed;
        [SerializeField] private Rigidbody2D rb;

        private Vector2 movement = Vector2.zero;
        private Vector2 lastDirection;

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
            if (status.IsCapableOfMovement())
            {
                getInputMovement();

                status.SetMovingStatus(isMoving());
            }
            else
                status.SetMovingStatus(false);
        }

        private void FixedUpdate()
        {
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

            rb.linearVelocity = normalizedMovement * speed.GetCurrentSpeed();

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
