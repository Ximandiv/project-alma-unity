using UnityEngine;
using Scripts.Scriptables;

namespace Scripts.UI
{
    public class MindDialogueTrigger : MonoBehaviour
    {
        public void StartDialogue()
        {
            if (dialogueCount < dialogues.Length)
            {
                currentDialogue = dialogues[dialogueCount]; //Selects the appropriate dialogue

                FindFirstObjectByType<DialogueController>().OpenDialogue(currentDialogue);

                dialogueCount++;
            }
            else
            {
                Debug.LogWarning("There is no more dialogue.");

            }
        }

        [SerializeField] private Dialogue[] dialogues;
        private Dialogue currentDialogue;
        private int dialogueCount = 0;
    }
}