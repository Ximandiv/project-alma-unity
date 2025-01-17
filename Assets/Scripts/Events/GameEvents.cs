using UnityEngine;
using System;
using Scripts.Scriptables;

namespace Scripts.Events
{
    public class GameEvents : MonoBehaviour
    {
        #region Public Variables

        public static GameEvents Instance;
        public event Action OnPause;
        public event Action OnUnpause;
        public event Action OnMenuToggle;
        public event Action<Dialogue> OnDialogueStart;
        public event Action<Dialogue> OnDialogueEnded;

        #endregion

        #region Public Methods
        
        public void Pause()
        {
            OnPause?.Invoke();
        }

        public void Unpause()
        {
            OnUnpause?.Invoke();
        }

        public void MenuToggle()
        {
            OnMenuToggle?.Invoke();
        }

        public void DialogueStart(Dialogue dialogue)
        {
            OnDialogueStart?.Invoke(dialogue);
        }

        public void DialogueEnded(Dialogue dialogue)
        {
            OnDialogueEnded?.Invoke(dialogue);
        }
        
        #endregion

        #region Unity API Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        #endregion
    }
}
