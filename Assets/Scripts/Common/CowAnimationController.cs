using UnityEngine;

namespace Scripts.Common
{
    public class CowAnimationController : MonoBehaviour
    {

        [SerializeField] private bool isSleeping;

        [Header("Facing Direction")]
        [Range(-1,1)]
        [SerializeField] private float x = -1;
        [Range(-1,1)]
        [SerializeField] private float y = 0;

        [SerializeField] private bool randomize;
        private Animator animator;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
           animator = GetComponent<Animator>();

           if (randomize)
           {
               isSleeping = Random.Range(0,2) == 1;
               x = Random.Range(-1.0f,1.0f);
               y = Random.Range(-1.0f,1.0f);

           }

           animator.SetBool("IsSleeping",isSleeping);
           animator.SetFloat("X",x); 
           animator.SetFloat("Y",y); 
        }

    }
}
