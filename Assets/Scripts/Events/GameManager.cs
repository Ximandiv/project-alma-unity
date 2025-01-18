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
            GameEvents.Instance.OnGameOver += handleGameOver;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnPaused -= handlePaused;
            GameEvents.Instance.OnUnpaused -= handleUnpaused;
            GameEvents.Instance.OnGameOver -= handleGameOver;
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

        private void handleGameOver()
        {
            SceneManager.LoadScene("Credits");
        }

        #endregion
    }
}
