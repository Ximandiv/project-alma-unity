using Scripts.Common;
using Scripts.Scriptables;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CharacterHitpoints))]
    [RequireComponent(typeof(CharacterStatus))]
    [RequireComponent(typeof(CharacterSpeed))]
    public class PlayerController : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private GameStatus gameStatus;

        private Transform flashlight;

        private AnimationController animController;
        private Health health;
        private Movement movement;

        private CharacterHitpoints hitpoints;
        private CharacterStatus status;
        private CharacterSpeed speed;

        private Rigidbody2D rbCollider;
        private Rigidbody2D rbHitbox;

        #endregion

        #region Public Variables

        [ContextMenu("Enter Combat")]
        public void EnterCombat()
            => flashlight.gameObject.SetActive(true);

        [ContextMenu("Exit Combat")]
        public void ExitCombat()
            => flashlight.gameObject.SetActive(false);

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            rbCollider = GetComponent<Rigidbody2D>();
            rbHitbox = transform.GetChild(1).GetComponent<Rigidbody2D>();
            status = GetComponent<CharacterStatus>();
            speed = GetComponent<CharacterSpeed>();
            hitpoints = GetComponent<CharacterHitpoints>();
            health = GetComponent<Health>();
            animController = transform.GetChild(0).GetComponent<AnimationController>();

            health.Initialize(this, hitpoints, status);

            initializeMovement();

            status.SetIsDead(false);
            status.SetCanMoveStatus(true);

            flashlight = GameObject.FindWithTag("Flashlight").transform;
        }

        #endregion

        #region Private Methods

        private void initializeMovement()
        {
            movement = GetComponent<Movement>();
            if (movement is not null)
                movement.Initialize(status, speed, gameStatus, rbCollider);

            if (animController is not null && movement is not null)
            {
                movement.onMoving += animController.SetMovement;
                movement.onFlip += animController.Flip;
            }
        }

        #endregion
    }
}