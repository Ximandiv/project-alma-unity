using Scripts.Scriptables;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        #region Private Variables

        private SpriteRenderer sprite;
        private Animator animator;

        #endregion

        #region Public Methods

        public void SetMovement(bool isMoving)
        {
            if (isMoving)
                animator.SetBool("isMoving", true);
            else
                animator.SetBool("isMoving", false);
        }

        public void Flip(bool isRotating)
            => sprite.flipX = isRotating;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        #endregion
    }
}
