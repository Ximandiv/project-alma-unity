using UnityEngine;
using Scripts.Scriptables;

namespace Scripts.Events
{
    public class StoryManager : MonoBehaviour
    {
        #region Private Variables
        
        [SerializeField] private StoryStatus storyStatus;

        #endregion

        #region Unity API Methods

        private void Start()
        {
            GameEvents.Instance.OnDialogueEnded += handleDialogueEnded;
            GameEvents.Instance.OnLevelBeaten += handleLevelBeaten;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnDialogueEnded -= handleDialogueEnded;
            GameEvents.Instance.OnLevelBeaten -= handleLevelBeaten;
        }

        #endregion

        #region Private Methods

        private void handleDialogueEnded(Dialogue endedDialogue)
        {
            //If the dialogue is a leading dialogue, changes the stage to the appropriate one.
            if (storyStatus.LeadingDialogues.TryGetValue(endedDialogue, out StoryStatus.StoryStage newStage))
            {
                storyStatus.ChangeStage(newStage);
            }
        
        }

        private void handleLevelBeaten()
        {
            storyStatus.ChangeStage(StoryStatus.StoryStage.Conclusion);
        }

        #endregion
    }
}
