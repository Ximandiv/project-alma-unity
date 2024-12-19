using UnityEngine;

namespace Scripts.Common
{
    public class CharacterHitpoints : MonoBehaviour
    {
        #region Public Methods

        #region Set Methods

        // Use this to configure the character's hitpoints on start
        public void SetCurrentHitpoints(int amount)
        {
            if (!RangeHelper.IsMinRangeCorrect(minHitPoints, amount))
                amount = minHitPoints;
            else if (!RangeHelper.IsMaxRangeCorrect(maxHitPoints, amount))
                amount = maxHitPoints;

            currentHitpoints = amount;
        }

        // Use this to configure the character's hitpoints on start
        public void SetMaxHitpoints(int amount)
        {
            if (!RangeHelper.IsMinRangeCorrect(minHitPoints, amount))
                amount = minHitPoints;

            maxHitPoints = amount;
        }

        #endregion

        // Use this to increment or decrement current hitpoints
        public void SumCurrentHitpoints(int amount)
        {
            currentHitpoints += amount;

            if (!RangeHelper.IsMinRangeCorrect(minHitPoints, currentHitpoints))
                currentHitpoints = minHitPoints;
            else if (!RangeHelper.IsMaxRangeCorrect(maxHitPoints, currentHitpoints))
                currentHitpoints = maxHitPoints;
        }

        #region Get Methods

        public int GetCurrentHitpoints() => currentHitpoints;
        public int GetMaxHitPoints() => maxHitPoints;
        public int GetMinHitPoints() => minHitPoints;

        #endregion

        #endregion

        #region Private Variables

        [SerializeField] private int currentHitpoints = 2;

        [SerializeField] private int minHitPoints = 1;
        [SerializeField] private int maxHitPoints = 2;

        #endregion
    }
}