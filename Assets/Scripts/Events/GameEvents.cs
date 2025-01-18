using UnityEngine;
using System;
using Scripts.Scriptables;

namespace Scripts.Events
{
    public class GameEvents : MonoBehaviour
    {
        #region Public Variables

        public static GameEvents Instance;
        public event Action OnPaused;
        public event Action OnUnpaused;
        public event Action OnTimerEnded;
        public event Action OnLevelStarted;
        public event Action OnLevelBeaten;
        public event Action OnMenuToggled;
        public event Action<Dialogue> OnDialogueStarted;
        public event Action<Dialogue> OnDialogueEnded;
        public event Action<String, String> OnPowerupObtained;
        public event Action OnGameOver;

        #endregion

        #region Public Methods
        
        public void Paused()
        {
            OnPaused?.Invoke();
        }

        public void Unpaused()
        {
            OnUnpaused?.Invoke();
        }

        public void TimerEnded()
        {
            OnTimerEnded?.Invoke();
        }

        public void LevelStarted()
        {
            OnLevelStarted?.Invoke();
        }

        public void LevelBeaten()
        {
            OnLevelBeaten?.Invoke();
        }

        public void MenuToggled()
        {
            OnMenuToggled?.Invoke();
        }

        public void DialogueStarted(Dialogue dialogue)
        {
            OnDialogueStarted?.Invoke(dialogue);
        }

        public void DialogueEnded(Dialogue dialogue)
        {
            OnDialogueEnded?.Invoke(dialogue);
        }

        public void PowerupObtained(string powerupName, string powerupDescription)
        {
            OnPowerupObtained?.Invoke(powerupName, powerupDescription);
        }

        public void GameOver()
        {
            OnGameOver?.Invoke();
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
