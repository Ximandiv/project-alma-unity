using UnityEngine;
using System;

namespace Scripts.UI
{
    public class MenuController : MonoBehaviour
    {
        #region Public Variables

        public event Action OnMenuOpen;
        public event Action OnMenuClosed;

        #endregion
        
        #region Public Methods

        public void QuitApp()
        {
            Application.Quit();
        }

        public void OpenMenu()
        {  
            OnMenuOpen?.Invoke();
            GameEvents.Instance.Pause();
            pausedPanel.SetActive(true);
        }

        public void CloseMenu()
        {  
            OnMenuClosed?.Invoke();
            GameEvents.Instance.Unpause();
            pausedPanel.SetActive(false);
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
        }

        private void Update()
        {
            if (Input.GetKeyDown(keyToMenu))
            {
                if (!pausedPanel.activeSelf) OpenMenu();
                else CloseMenu();
            }

        }

        #endregion

    }
}

