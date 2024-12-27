using UnityEngine;
using Scripts.Scriptables;

namespace Scripts.UI
{
    public class PauseButtonController : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private GameStatus gameStatus;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private KeyCode keyToMenu = KeyCode.Escape;

        #endregion
        
        #region Unity API Methods

        private void Update()
        {
            if (Input.GetKeyDown(keyToMenu))
            {
                activateButton();
            }
        }

        #endregion

        #region Private Methods

        private void activateButton()
        {  
            gameStatus.Pause();
            menuPanel.SetActive(!menuPanel.activeSelf);
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
        
        #endregion
    }
}

