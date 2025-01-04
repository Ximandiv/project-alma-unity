using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Story Status", menuName = "Scriptables/StoryStatus")]
    public class StoryStatus : ScriptableObject
    {
        public enum StoryStage
        {
            Start, // Game starting point
            Conversation1, // Active after talking with Emilio
            Conversation2, // Active after talking with Ferney's Family
            Conversation3,  // Active after talking with Ferney
            Conclusion // Active after finishing Ferney's mind level
        }

        public StoryStage CurrentStage = StoryStage.Start;

        public void changeStage(StoryStage newStage)
        {
            CurrentStage = newStage;
        }

    }
}
