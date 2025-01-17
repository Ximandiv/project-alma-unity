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
        private MindDialogueTrigger mindDialogueTrigger;
        private PowerupPopupController powerupPopupController;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            menuController = FindFirstObjectByType<MenuController>();
            dialogueController = FindFirstObjectByType<DialogueController>();
            powerupPopupController = FindFirstObjectByType<PowerupPopupController>();
            mindDialogueTrigger = FindFirstObjectByType<MindDialogueTrigger>();
        }

        private void Start()
        {
            GameEvents.Instance.OnMenuToggled += handleMenuToggled;
            GameEvents.Instance.OnDialogueStarted += handleDialogueStarted;
            GameEvents.Instance.OnPowerupObtained += handlePowerupObtained;
            GameEvents.Instance.OnLevelStarted += handleLevelStarted;
            GameEvents.Instance.OnTimerEnded += handleTimerEnded;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnMenuToggled -= handleMenuToggled;
            GameEvents.Instance.OnDialogueStarted -= handleDialogueStarted;
            GameEvents.Instance.OnPowerupObtained -= handlePowerupObtained;
            GameEvents.Instance.OnLevelStarted -= handleLevelStarted;
            GameEvents.Instance.OnTimerEnded -= handleTimerEnded;
        }

        #endregion

        #region Private Methods

        private void handleMenuToggled()
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

        private void handleDialogueStarted(Dialogue dialogue)
        {
            dialogueController.OpenDialogue(dialogue);
        }

        private void handlePowerupObtained(string powerupName, string powerupDescription)
        {
            powerupPopupController.OpenPowerupPopup(powerupName, powerupDescription);
        }

        private void handleLevelStarted()
        {
            mindDialogueTrigger.StartDialogue();
        }

        private void handleTimerEnded()
        {
            mindDialogueTrigger.StartDialogue();
        }

        #endregion
    }
}