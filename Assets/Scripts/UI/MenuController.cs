using UnityEngine;

namespace Scripts.UI
{
    public class MenuController : MonoBehaviour
    {
        #region Public Variables
        public bool IsOpen;

        #endregion
        
        #region Public Methods

        public void QuitApp()
        {
            Application.Quit();
        }

        public void OpenMenu()
        {  
            GameEvents.Instance.Pause();
            pausedPanel.SetActive(true);
            IsOpen = true;
        }

        public void CloseMenu()
        {  
            GameEvents.Instance.Unpause();
            pausedPanel.SetActive(false);
            IsOpen = false;
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
                GameEvents.Instance.MenuToggle();
            }

        }

        #endregion

    }
}

