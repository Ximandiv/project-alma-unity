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
        private GameOverUIController gameOverUIController;
        private InGameHelpController inGameHelpController;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            menuController = FindFirstObjectByType<MenuController>();
            dialogueController = FindFirstObjectByType<DialogueController>();
            powerupPopupController = FindFirstObjectByType<PowerupPopupController>();
            mindDialogueTrigger = FindFirstObjectByType<MindDialogueTrigger>();
            gameOverUIController = FindFirstObjectByType<GameOverUIController>();
            inGameHelpController = FindFirstObjectByType<InGameHelpController>();
        }

        private void Start()
        {
            GameEvents.Instance.OnGameStarted += handleGameStarted;
            GameEvents.Instance.OnMenuToggled += handleMenuToggled;
            GameEvents.Instance.OnDialogueStarted += handleDialogueStarted;
            GameEvents.Instance.OnPowerupObtained += handlePowerupObtained;
            GameEvents.Instance.OnLevelStarted += handleLevelStarted;
            GameEvents.Instance.OnTimerEnded += handleTimerEnded;
            GameEvents.Instance.OnGameOver += handleGameOver;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnGameStarted -= handleGameStarted;
            GameEvents.Instance.OnMenuToggled -= handleMenuToggled;
            GameEvents.Instance.OnDialogueStarted -= handleDialogueStarted;
            GameEvents.Instance.OnPowerupObtained -= handlePowerupObtained;
            GameEvents.Instance.OnLevelStarted -= handleLevelStarted;
            GameEvents.Instance.OnTimerEnded -= handleTimerEnded;
            GameEvents.Instance.OnGameOver -= handleGameOver;
        }

        #endregion

        #region Private Methods

        private void handleGameStarted()
        {
            inGameHelpController.OpenVillageHelp();
        }
        
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
            inGameHelpController.OpenMindHelp();
        }

        private void handleTimerEnded()
        {
            mindDialogueTrigger.StartDialogue();
        }

        private void handleGameOver()
        {
            gameOverUIController.OpenGameOverScreen();
        }

        #endregion
    }
}