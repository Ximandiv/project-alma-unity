using Scripts.Scriptables;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Listener
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractionTrigger : MonoBehaviour 
    {
        #region Private Variables
            [SerializeField] private GameStatus gameStatus;
            [SerializeField] private UnityEvent onInteractionPerformed = new UnityEvent();
            [SerializeField] private UnityEvent onPlayerEntered = new UnityEvent();
            [SerializeField] private UnityEvent onPlayerExited = new UnityEvent();
            [SerializeField] private KeyCode keyToInteract = KeyCode.E;
            private bool playerInRange = false;
        
        #endregion

       #region Private Methods
         private void Update() 
         {
             if (playerInRange)
             {
                 if (Input.GetKeyDown(keyToInteract) && !gameStatus.IsPaused)
                     onInteractionPerformed.Invoke();
             }
         
         }
         private void OnTriggerEnter2D(Collider2D other) 
         {
             if (playerInRange) return;
 
                playerInRange = true;
                onPlayerEntered.Invoke(); 
         }
 
         private void OnTriggerExit2D(Collider2D other) 
         {
             if (!playerInRange) return;
 
             playerInRange = false;
             onPlayerExited.Invoke();   
         }
       #endregion

    }
}