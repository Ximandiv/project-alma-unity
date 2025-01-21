using UnityEngine;
using Scripts.Scriptables;
using UnityEngine.SceneManagement;

namespace Scripts.Events
{
    public class GameManager : MonoBehaviour
    {
        #region Private Variables
        
        [SerializeField] private GameStatus gameStatus;
        private SceneLoader sceneLoader;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            sceneLoader = FindFirstObjectByType<SceneLoader>();
        }

        private void Start()
        {
            GameEvents.Instance.OnPaused += handlePaused;
            GameEvents.Instance.OnUnpaused += handleUnpaused;
            GameEvents.Instance.OnSceneChanged += handleSceneChanged;
            GameEvents.Instance.OnGameWon += handleGameWon;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnPaused -= handlePaused;
            GameEvents.Instance.OnUnpaused -= handleUnpaused;
            GameEvents.Instance.OnSceneChanged -= handleSceneChanged;
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

        private void handleSceneChanged(string sceneName)
        {
            sceneLoader.changeScene(sceneName);
        }

        private void handleGameWon()
        {
            GameEvents.Instance.SceneChanged("Credits");
        }

        #endregion
    }
}
