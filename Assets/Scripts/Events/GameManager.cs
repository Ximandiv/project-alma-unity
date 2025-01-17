using UnityEngine;
using Scripts.Scriptables;

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
            GameEvents.Instance.OnPause += handlePause;
            GameEvents.Instance.OnUnpause += handleUnpause;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnPause -= handlePause;
            GameEvents.Instance.OnUnpause -= handleUnpause;
        }

        #endregion

        #region Private Methods

        private void handlePause()
        {
            gameStatus.Pause();
        }

        private void handleUnpause()
        {
            gameStatus.Unpause();
        }

        #endregion
    }
}
