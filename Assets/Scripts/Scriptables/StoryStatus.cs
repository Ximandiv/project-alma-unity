using UnityEngine;
using System.Collections.Generic;
using Scripts.UI;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Story Status", menuName = "Scriptables/StoryStatus")]
    public class StoryStatus : ScriptableObject
    {
        #region Public Variables
        public enum StoryStage
        {
            Start, // Game starting point
            Conversation1, // Active after talking with Emilio
            Conversation2, // Active after talking with Ferney's Family
            Conversation3,  // Active after talking with Ferney
            Conclusion // Active after finishing Ferney's mind level
        }

        public StoryStage CurrentStage = StoryStage.Start;
        public Dictionary<Dialogue, StoryStatus.StoryStage> LeadingDialogues;

        #endregion
        
        #region Public Methods

        public void ChangeStage(StoryStage newStage)
        {
            CurrentStage = newStage;
        }

        #endregion

        #region Private Variables

        [SerializeField] private DialogueMapping[] leadingDialoguesMappings;

        #endregion

        #region Unity API Methods
        
        private void OnEnable()
        {
            CurrentStage = StoryStage.Start;
            
            LeadingDialogues = new Dictionary<Dialogue, StoryStatus.StoryStage>();

            //Uses mapping created in the editor to fill the dictionary.
            foreach (var mapping in leadingDialoguesMappings)
            {
                LeadingDialogues[mapping.dialogue] = mapping.storyStage;
            }
        }

        #endregion

    }
}
