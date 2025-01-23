using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "NPCAttempts", menuName = "Scriptables/NPCAttempts")]
    public class NPCAttempts : ScriptableObject
    {
        #region Attempts

        [Header("Attempts")]

        [SerializeField] private int currentAttempts = 3;
        public int CurrentAttempts
        {
            get { return currentAttempts; }
            set { currentAttempts = value; }
        }

        [SerializeField] private int minAttempts = 1;
        public int MinAttempts { get { return minAttempts; } }

        [SerializeField] private int maxAttempts = 3;
        public int MaxAttempts
        {
            get { return maxAttempts; }
            set { maxAttempts = value; }
        }

        #endregion
    }
}