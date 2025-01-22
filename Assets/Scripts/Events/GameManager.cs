using UnityEngine;
using Scripts.Scriptables;
using UnityEngine.SceneManagement;

namespace Scripts.Events
{
    public class GameManager : MonoBehaviour
    {
        #region Private Variables
        
        [SerializeField] private GameStatus gameStatus;

        #endregion

        #region Unity API Methods

        private void Start()
        {
            GameEvents.Instance.OnPaused += handlePaused;
            GameEvents.Instance.OnUnpaused += handleUnpaused;
            SceneManager.sceneLoaded += handleSceneLoaded;
            GameEvents.Instance.OnGameWon += handleGameWon;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnPaused -= handlePaused;
            GameEvents.Instance.OnUnpaused -= handleUnpaused;
            SceneManager.sceneLoaded -= handleSceneLoaded;
            GameEvents.Instance.OnGameWon -= handleGameWon;
        }

        #endregion

        #region Private Methods

        private void handlePaused()
        {
            gameStatus.Pause();
        }

        private void handleUnpaused()
        {
            gameStatus.Unpause();
        }

        private void handleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Village" && !gameStatus.HasStarted)
            {
                GameEvents.Instance.GameStarted();
                gameStatus.HasStarted = true;
            }
            else if (scene.name == "MindLevel")
            {
                GameEvents.Instance.LevelStarted();
            }
        }

        private void handleGameWon()
        {
            GameEvents.Instance.SceneChanged("Credits");
        }

        #endregion
    }
}
