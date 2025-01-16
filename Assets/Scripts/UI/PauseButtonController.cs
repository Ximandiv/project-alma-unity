using UnityEngine;
using Scripts.Scriptables;

namespace Scripts.UI
{
    public class PauseButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseButtonPanel;
        private MenuController menuController;

        private void Awake()
        {
            menuController = FindFirstObjectByType<MenuController>();
            pauseButtonPanel.SetActive(true);
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
            pauseButtonPanel.SetActive(false);
        }

        private void handleMenuClosed()
        {
            pauseButtonPanel.SetActive(true);
        }

    }
}

