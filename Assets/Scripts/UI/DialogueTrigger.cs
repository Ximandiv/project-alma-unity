using UnityEngine;
using Scripts.Scriptables;

namespace Scripts.UI
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue dialogue;

        public void StartDialogue()
        {
            FindFirstObjectByType<DialogueManager>().OpenDialogue(dialogue);
        }

    }
}
