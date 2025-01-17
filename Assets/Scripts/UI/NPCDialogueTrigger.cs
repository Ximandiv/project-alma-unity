using UnityEngine;
using Scripts.Scriptables;
using System.Collections.Generic;
using Scripts.Events;

namespace Scripts.UI
{
    [RequireComponent(typeof(Collider2D))]
    public class NPCDialogueTrigger : MonoBehaviour
    {     
        #region Public Methods

        public void StartDialogue()
        {
            //Checks if a dialogue exist for current story stage.
            if (dialogues.TryGetValue(storyStatus.CurrentStage, out currentDialogue))
            {
                if (currentDialogue == null)
                {
                    Debug.LogWarning("Missing dialogue reference for current story stage.");
                }
                else
                {
                    GameEvents.Instance.DialogueStarted(currentDialogue);
                } 
            }
            else
            {
                Debug.LogWarning("No dialogue found for current story stage.");
            }
        }

        #endregion

        #region Private Variables

        [SerializeField] private GameStatus gameStatus;
        [SerializeField] private StoryStatus storyStatus;
        [SerializeField] private GameObject visualCue;
        [SerializeField] private KeyCode keyToTalk = KeyCode.E;
        [SerializeField] private DialogueMapping[] dialogueMappings;
        private bool playerInRange;
        private Dictionary<StoryStatus.StoryStage, Dialogue> dialogues;
        private Dialogue currentDialogue;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            playerInRange = false;
            visualCue.SetActive(false);
            
            dialogues = new Dictionary<StoryStatus.StoryStage, Dialogue>();
            
            //Uses mapping created in the editor to fill the dictionary.
            foreach (var mapping in dialogueMappings)
            {
                dialogues[mapping.storyStage] = mapping.dialogue;
            }
        }

        private void Update()
        {
            if (playerInRange)
            {
                visualCue.SetActive(true);
                if (Input.GetKeyDown(keyToTalk) && !gameStatus.IsPaused)
                {
                    StartDialogue();
                }
            }
            else
            {
                visualCue.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerInRange = false;
            }
        }

        #endregion

    }
}
