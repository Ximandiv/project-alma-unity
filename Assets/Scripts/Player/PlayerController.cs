using Scripts.Common;
using Scripts.Scriptables;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(AnimationController))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CharacterStatus))]
    [RequireComponent(typeof(CharacterSpeed))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameStatus gameStatus;
        private Transform flashlight;
        private AnimationController animController;
        private Movement movement;
        private Rigidbody2D rb;
        private CharacterStatus status;
        private CharacterSpeed speed;

        [ContextMenu("Enter Combat")]
        public void EnterCombat()
            => flashlight.gameObject.SetActive(true);

        [ContextMenu("Exit Combat")]
        public void ExitCombat()
            => flashlight.gameObject.SetActive(false);

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            status = GetComponent<CharacterStatus>();
            speed = GetComponent<CharacterSpeed>();

            animController = transform.GetChild(0).GetComponent<AnimationController>();

            InitializeMovement();

            flashlight = GameObject.FindWithTag("Flashlight").transform;
            flashlight.gameObject.SetActive(false);
        }

        private void InitializeMovement()
        {
            movement = GetComponent<Movement>();
            if (movement is not null)
                movement.Initialize(status, speed, gameStatus, rb);

            if (animController is not null && movement is not null)
            {
                movement.onMoving += animController.SetMovement;
                movement.onFlip += animController.Flip;
            }
        }
    }
}