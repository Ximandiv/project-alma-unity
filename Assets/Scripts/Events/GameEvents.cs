using UnityEngine;
using System;
using Scripts.Scriptables;

namespace Scripts.Events
{
    public class GameEvents : MonoBehaviour
    {
        #region Public Variables

        public static GameEvents Instance;
        public event Action OnGameStarted;
        public event Action OnPaused;
        public event Action OnUnpaused;
        public event Action<string> OnSceneChanged;
        public event Action<float> OnTimerUpdated;
        public event Action OnTimerEnded;
        public event Action OnLevelStarted;
        public event Action OnLevelBeaten;
        public event Action OnMenuOpen;
        public event Action OnMenuClosed;
        public event Action<Dialogue> OnDialogueStarted;
        public event Action<Dialogue, int> OnDialogueAdvance;
        public event Action<Dialogue> OnDialogueEnded;
        public event Action<String, String> OnPowerupObtained;
        public event Action OnGameOver;
        public event Action OnGameWon;

        #endregion

        #region Public Methods
        
        public void GameStarted()
        {
            OnGameStarted?.Invoke();
        }
        public void Paused()
        {
            OnPaused?.Invoke();
        }

        public void Unpaused()
        {
            OnUnpaused?.Invoke();
        }

        public void SceneChanged(string sceneName)
        {
            OnSceneChanged?.Invoke(sceneName);
        }

        public void TimerUpdated(float remainingTime)
        {
            OnTimerUpdated?.Invoke(remainingTime);
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

        public void MenuOpen()
        {
            OnMenuOpen?.Invoke();
        }

        public void MenuClosed()
        {
            OnMenuClosed?.Invoke();
        }

        public void DialogueStarted(Dialogue dialogue)
        {
            OnDialogueStarted?.Invoke(dialogue);
        }
        public void DialogueAdvance(Dialogue dialogue, int index)
        {
            OnDialogueAdvance?.Invoke(dialogue,index);
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

        public void GameWon()
        {
            OnGameWon?.Invoke();
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

            DontDestroyOnLoad(gameObject);
        }

        #endregion
    }
}
