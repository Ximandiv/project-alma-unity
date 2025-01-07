using UnityEngine;
using Scripts.Scriptables;

namespace Scripts.UI
{
    public class PauseButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        private MenuController menuController;

        private void Awake()
        {
            menuController = FindFirstObjectByType<MenuController>();
        }
        
        private void OnEnable()
        {
            menuController.OnMenuOpen += handleMenuOpen;
            menuController.OnMenuClosed += handleMenuClosed;
        }

        private void OnDisable()
        {
            menuController.OnMenuOpen -= handleMenuOpen;
            menuController.OnMenuClosed -= handleMenuClosed;
        }

        private void handleMenuOpen()
        {
            pausePanel.SetActive(false);
        }

        private void handleMenuClosed()
        {
            pausePanel.SetActive(true);
        }

    }
}

