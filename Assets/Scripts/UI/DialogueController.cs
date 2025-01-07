using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Scriptables;
using System;

namespace Scripts.UI
{
    public class DialogueController : MonoBehaviour
    {
        #region Public Variables

        public event Action<Dialogue> OnDialogueEnded;

        #endregion
        
        #region Public Methods
        
        public void OpenDialogue(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            currentMessages = dialogue.Messages;
            currentActors = dialogue.Actors;
            activeMessage = 0;

            gameStatus.Pause();
            isActive = true;
            dialoguePanel.SetActive(true);
            menuController.OnMenuOpen += handleMenuOpen;
            menuController.OnMenuClosed += handleMenuClosed;

            displayMessage();
        }

        public void NextMessage()
        {
            activeMessage++;
            if (activeMessage < currentMessages.Length) 
            {
                displayMessage();
            } else
            {
                OnDialogueEnded?.Invoke(currentDialogue);

                if (currentDialogue.IsLevelStarter)
                {
                    openChoices(choicesDialogue);
                }
                else if(currentDialogue != choicesDialogue)
                {
                    closeDialogue();
                }
            }
        }

        public void CloseChoices()
        {
            continueButton.gameObject.SetActive(true);
            helpButton.gameObject.SetActive(false);
            afterButton.gameObject.SetActive(false);

            closeDialogue();
        }

        #endregion

        #region Private Variables

        [Header("GameStatus")]
        [SerializeField] private GameStatus gameStatus;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI displayedName;
        [SerializeField] private Image displayedSprite;
        [SerializeField] private TextMeshProUGUI displayedText;
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button helpButton;
        [SerializeField] private Button afterButton;

        [Header("Key Binding")]
        [SerializeField] private KeyCode keyToNext = KeyCode.Space;

        [Header("Choices Dialogue")]
        [SerializeField] private Dialogue choicesDialogue;
        
        private MenuController menuController;
        private Dialogue currentDialogue;
        private Dialogue.Message[] currentMessages;
        private Dialogue.Actor[] currentActors;
        private int activeMessage;
        private static bool isActive;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            isActive = false;
            
            continueButton.gameObject.SetActive(true);
            helpButton.gameObject.SetActive(false);
            afterButton.gameObject.SetActive(false);
            dialoguePanel.SetActive(false);

            menuController = FindFirstObjectByType<MenuController>();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(keyToNext) &&  isActive == true && continueButton.interactable)
            {
                continueButton.onClick.Invoke();
            }
        }

        #endregion

        #region Private Methods

        private void displayMessage()
        {
            Dialogue.Message messageToDisplay = currentMessages[activeMessage];
            displayedText.text = messageToDisplay.MessageText;

            Dialogue.Actor actorToDisplay = currentActors[messageToDisplay.ActorId];
            displayedName.text = actorToDisplay.ActorName;
            displayedSprite.sprite = actorToDisplay.ActorSprite;
        }

        private void openChoices(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            currentMessages = dialogue.Messages;
            currentActors = dialogue.Actors;
            activeMessage = 0;

            continueButton.gameObject.SetActive(false);
            helpButton.gameObject.SetActive(true);
            afterButton.gameObject.SetActive(true);

            displayMessage();
        }

        private void closeDialogue()
        {
            gameStatus.Unpause();
            isActive = false;
            dialoguePanel.SetActive(false);
            menuController.OnMenuOpen -= handleMenuOpen;
            menuController.OnMenuClosed -= handleMenuClosed;
        }

        private void handleMenuOpen()
        {
            continueButton.interactable = false;
            helpButton.interactable = false;
            afterButton.interactable = false;
        }

        private void handleMenuClosed()
        {
            continueButton.interactable = true;
            helpButton.interactable = true;
            afterButton.interactable = true;
        }

        #endregion
        
    }
}
