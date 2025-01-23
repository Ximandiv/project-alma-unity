using UnityEngine;
using Scripts.Events;

namespace Scripts.UI
{
    public class MenuController : MonoBehaviour
    {
        #region Public Variables
        [HideInInspector] public bool IsOpen;

        #endregion
        
        #region Public Methods

        public void OpenMenu()
        {  
            GameEvents.Instance.MenuOpen();
            GameEvents.Instance.Paused();
            pausedPanel.SetActive(true);
            IsOpen = true;
        }

        public void CloseMenu()
        {  
            GameEvents.Instance.MenuClosed();
            GameEvents.Instance.Unpaused();
            pausedPanel.SetActive(false);
            IsOpen = false;
        }

        public void QuitApp()
        {
            Application.Quit();
        }

        #endregion
        
        #region Private Variables

        [SerializeField] private GameObject pausedPanel;
        [SerializeField] private KeyCode keyToMenu = KeyCode.Escape;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            pausedPanel.SetActive(false);
            IsOpen = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(keyToMenu))
            {
                if(!IsOpen)
                {
                    OpenMenu();
                }
                else 
                {
                    CloseMenu();
                }
            }

        }

        #endregion

    }
}

