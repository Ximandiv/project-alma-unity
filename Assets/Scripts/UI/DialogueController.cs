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
        
        public void OpenDialogue(Dialogue dialogue)
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

        public void NextMessage()
        {
            AudioManager.Instance.PlaySFX(clickClip, clickSFXVolumen);
            activeMessageIndex++;
            if (activeMessageIndex < currentMessages.Length) 
            {
                displayMessage();
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

        public void CloseChoices()
        {
            AudioManager.Instance.PlaySFX(clickClip, clickSFXVolumen);
            continueButton.gameObject.SetActive(true);
            helpButton.gameObject.SetActive(false);
            afterButton.gameObject.SetActive(false);

            closeDialogue();
        }

        public void DisableDialogue()
        {
            continueButton.interactable = false;
            helpButton.interactable = false;
            afterButton.interactable = false;
        }

        public void EnableDialogue()
        {
            continueButton.interactable = true;
            helpButton.interactable = true;
            afterButton.interactable = true;
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
        
        private Dialogue currentDialogue;
        private Dialogue.Message[] currentMessages;
        private Dialogue.Actor[] currentActors;
        private int activeMessageIndex;

        [Header("SFX")]
        [SerializeField] private AudioClip clickClip;
        [SerializeField] private float clickSFXVolumen = 1f;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            IsOpen = false;

            continueButton.gameObject.SetActive(true);
            helpButton.gameObject.SetActive(false);
            afterButton.gameObject.SetActive(false);
            dialoguePanel.SetActive(false);
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

        private void displayMessage()
        {
            Dialogue.Message activeMessage = currentMessages[activeMessageIndex];

            //Uses the TMP's Typewriter to type the text from active message
            Typewriter typewriter = displayedText.GetComponent<Typewriter>();
            typewriter.SetText(activeMessage.MessageText);

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

        #endregion
        
    }
}
