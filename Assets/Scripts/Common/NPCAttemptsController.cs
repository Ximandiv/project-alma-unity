using Scripts.Scriptables;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Common
{
    public class NPCAttemptsController : MonoBehaviour
    {
        #region Public Methods

        #region Set Methods

        // Use this to configure the NPC's Attempts
        public void SetCurrentAttempts(int amount)
        {
            if (amount > NPCAttempts.MaxAttempts)
                amount = NPCAttempts.MaxAttempts;
            else if (amount < NPCAttempts.MinAttempts)
                amount = NPCAttempts.MinAttempts;

            NPCAttempts.CurrentAttempts = amount;
        }

        // Use this to increment or decrement current attempts
        public void SumCurrentAttempts(int amount)
        {
            NPCAttempts.CurrentAttempts += amount;

            if (!RangeHelper.IsMinRangeCorrect(NPCAttempts.MinAttempts, NPCAttempts.CurrentAttempts))
                NPCAttempts.CurrentAttempts = NPCAttempts.MinAttempts;
            else if (!RangeHelper.IsMaxRangeCorrect(NPCAttempts.MaxAttempts, NPCAttempts.CurrentAttempts))
                NPCAttempts.CurrentAttempts = NPCAttempts.MaxAttempts;
        }


        #endregion

        #region Get Methods

        public int GetCurrentAttempts() => NPCAttempts.CurrentAttempts;
        public int GetMaxAttempts() => NPCAttempts.MaxAttempts;
        public int GetMinAttempts() => NPCAttempts.MinAttempts;

        #endregion

        #endregion

        #region Private Variables

        [SerializeField] private NPCAttempts NPCAttempts;

        #endregion

        #region Private Methods

        private void Awake()
        {
            if (NPCAttempts.CurrentAttempts == 0) NPCAttempts.CurrentAttempts = NPCAttempts.MaxAttempts;
        }

        #endregion
    }
}