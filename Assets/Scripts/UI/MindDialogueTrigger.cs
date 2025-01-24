using UnityEngine;
using Scripts.Scriptables;
using Scripts.Events;

namespace Scripts.UI
{
    public class MindDialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue[] dialogues;
        private Dialogue currentDialogue;
        private int dialogueCount = 0;

        private void OnEnable()
        {
            //GameEvents.Instance.OnLevelStarted += startDialogue;
            //GameEvents.Instance.OnTimerEnded += startDialogue;
        }

        private void OnDestroy()
        {
            //GameEvents.Instance.OnLevelStarted -= startDialogue;
            //GameEvents.Instance.OnTimerEnded -= startDialogue;
        }

        private void startDialogue()
        {
            if (dialogueCount < dialogues.Length)
            {
                currentDialogue = dialogues[dialogueCount]; //Selects the appropriate dialogue

                GameEvents.Instance.DialogueStarted(currentDialogue);

                dialogueCount++;
            }
            else
            {
                Debug.LogWarning("There is no more dialogue.");
            }
        }
    }
}