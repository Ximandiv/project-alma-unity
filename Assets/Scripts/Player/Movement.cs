using Scripts.Common;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Controller))]
    public class Movement : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private Controller controller;
        [SerializeField] private Rigidbody2D rb;

        private Vector2 movement = Vector2.zero;
        private Vector2 lastDirection;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            controller = GetComponent<Controller>();
            
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (controller.GetCanMove())
            {
                getInputMovement();

                controller.SetMovingStatus(isMoving());
            }
            else
                controller.SetMovingStatus(false);
        }

        private void FixedUpdate()
        {
            if (controller.GetCanMove())
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

            rb.linearVelocity = normalizedMovement * controller.GetCurrentSpeed();

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
