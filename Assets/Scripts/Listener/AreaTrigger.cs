using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Listener
{
    [RequireComponent(typeof(Collider2D))]
    public class AreaTrigger : MonoBehaviour 
    {
        #region Private Variables

            [SerializeField] private int maxTriggerCount = 1;
            [SerializeField] private UnityEvent onPlayerEntered = new UnityEvent();
            private int triggerCount = 0;

        #endregion
        #region Monobehaviour implementation
            private void OnTriggerEnter2D(Collider2D other) 
            {
                if (triggerCount == maxTriggerCount || !other.gameObject.CompareTag("Player"))
                    return;
    
                onPlayerEntered.Invoke();
    
                if (maxTriggerCount >= 0)
                    triggerCount++;
            
            }
        #endregion
    
    }
}