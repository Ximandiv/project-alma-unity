using Scripts.Scriptables;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Events
{
    public class UIManager : MonoBehaviour
    {
        #region Private Variables

        private MenuController menuController;
        private DialogueController dialogueController;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            menuController = FindFirstObjectByType<MenuController>();
            dialogueController = FindFirstObjectByType<DialogueController>();
        }

        private void Start()
        {
            GameEvents.Instance.OnMenuToggle += handleMenuToggle;
            GameEvents.Instance.OnDialogueStart += handleDialogueStart;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnMenuToggle -= handleMenuToggle;
            GameEvents.Instance.OnDialogueStart -= handleDialogueStart;
        }

        #endregion

        #region Private Methods

        private void handleMenuToggle()
        {
            if(!menuController.IsOpen)
            {
                menuController.OpenMenu();
                if (dialogueController.IsOpen) dialogueController.DisableDialogue();
            }
            else 
            {
                menuController.CloseMenu();
                if (dialogueController.IsOpen) dialogueController.EnableDialogue();
            }
        }

        private void handleDialogueStart(Dialogue dialogue)
        {
            dialogueController.OpenDialogue(dialogue);
        }

        #endregion
    }
}