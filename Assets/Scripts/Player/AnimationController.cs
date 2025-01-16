using Scripts.Scriptables;
using UnityEngine;

namespace Scripts.Player
{
    public class AnimationController : MonoBehaviour
    {
        private SpriteRenderer sprite;
        private Animator animator;

        public void SetMovement(bool isMoving)
        {
            if (isMoving)
                animator.SetBool("isMoving", true);
            else
                animator.SetBool("isMoving", false);
        }

        public void Flip(bool isRotating)
            => sprite.flipX = isRotating;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }
    }
}
