using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Scriptables;

namespace Scripts.UI
{
    public class DialogueController : MonoBehaviour
    {
        #region Public Methods
        
        public void OpenDialogue(Dialogue dialogue)
        {
            currentMessages = dialogue.Messages;
            currentActors = dialogue.Actors;
            activeMessage = 0;

            gameStatus.Pause();
            isActive = true;
            dialoguePanel.SetActive(!dialoguePanel.activeSelf);

            displayMessage();
        }

        public void NextMessage()
        {
            activeMessage++;
            if (activeMessage < currentMessages.Length) 
            {
                displayMessage();
            } else {
                gameStatus.Pause();
                isActive = false;
                dialoguePanel.SetActive(!dialoguePanel.activeSelf);
            }
        }

        #endregion

        #region Private Variables

        [SerializeField] private GameStatus gameStatus;
        [SerializeField] private TextMeshProUGUI displayedName;
        [SerializeField] private Image displayedSprite;
        [SerializeField] private TextMeshProUGUI displayedText;
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private KeyCode keyToNext = KeyCode.Space;
        
        private Dialogue.Message[] currentMessages;
        private Dialogue.Actor[] currentActors;
        private int activeMessage = 0;
        private static bool isActive = false;

        #endregion

        #region Unity API Methods

        private void Update()
        {
            if (Input.GetKeyDown(keyToNext) && isActive == true)
            {
                NextMessage();
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

        #endregion
        
    }
}
