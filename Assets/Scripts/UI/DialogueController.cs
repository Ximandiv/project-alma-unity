using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Scriptables;
using Scripts.Events;

namespace Scripts.UI
{
    public class DialogueController : MonoBehaviour
    {
        #region Public Variables

        [HideInInspector] public bool IsOpen;

        #endregion
        
        #region Public Methods

        public void NextMessage()
        {
            if (typeWriter.IsTyping)
            {
                typeWriter.CompleteType();
                return;
            }

            activeMessageIndex++;
            if (activeMessageIndex < currentMessages.Length) 
            {
                displayMessage();
                GameEvents.Instance.DialogueAdvance(currentDialogue,activeMessageIndex);
            } else
            {
                GameEvents.Instance.DialogueEnded(currentDialogue);

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

        public void ChooseHelp()
        {
            CloseChoices();
            GameEvents.Instance.SceneChanged("MindLevel");
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
        

        private Typewriter typeWriter;
        private Dialogue currentDialogue;
        private Dialogue.Message[] currentMessages;
        private Dialogue.Actor[] currentActors;
        private int activeMessageIndex;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            typeWriter = displayedText.GetComponent<Typewriter>();
            IsOpen = false;

            continueButton.gameObject.SetActive(true);
            helpButton.gameObject.SetActive(false);
            afterButton.gameObject.SetActive(false);
            dialoguePanel.SetActive(false);
        }

        private void OnEnable()
        {
            GameEvents.Instance.OnDialogueStarted += openDialogue;
            GameEvents.Instance.OnMenuOpen += disableDialogue;
            GameEvents.Instance.OnMenuClosed += enableDialogue;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnDialogueStarted -= openDialogue;
            GameEvents.Instance.OnMenuOpen -= disableDialogue;
            GameEvents.Instance.OnMenuClosed -= enableDialogue;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(keyToNext) &&  IsOpen == true && continueButton.interactable)
            {
                continueButton.onClick.Invoke();
            }
        }

        #endregion

        #region Private Methods

        private void openDialogue(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            currentMessages = dialogue.Messages;
            currentActors = dialogue.Actors;
            activeMessageIndex = 0;

            GameEvents.Instance.Paused();
            IsOpen = true;
            dialoguePanel.SetActive(true);

            displayMessage();
        }

        private void displayMessage()
        {
            Dialogue.Message activeMessage = currentMessages[activeMessageIndex];

            //Uses the TMP's Typewriter to type the text from active message
            typeWriter.SetText(activeMessage.MessageText);

            Dialogue.Actor activeActor = currentActors[activeMessage.ActorId];

            //Sets the values ​​of the active actor to be displayed
            displayedName.text = activeActor.ActorName;
            displayedSprite.sprite = activeActor.ActorSprite;
        }

        private void openChoices(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            currentMessages = dialogue.Messages;
            currentActors = dialogue.Actors;
            activeMessageIndex = 0;

            continueButton.gameObject.SetActive(false);
            helpButton.gameObject.SetActive(true);
            afterButton.gameObject.SetActive(true);

            displayMessage();
        }

        private void closeDialogue()
        {
            GameEvents.Instance.Unpaused();
            IsOpen = false;
            dialoguePanel.SetActive(false);
        }

        private void disableDialogue()
        {
            continueButton.interactable = false;
            helpButton.interactable = false;
            afterButton.interactable = false;
        }

        private void enableDialogue()
        {
            continueButton.interactable = true;
            helpButton.interactable = true;
            afterButton.interactable = true;
        }

        #endregion
        
    }
}
