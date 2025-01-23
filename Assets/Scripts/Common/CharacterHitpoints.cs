using Scripts.Scriptables;
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
            if (!RangeHelper.IsMinRangeCorrect(characterStats.MinHitPoints, amount))
                amount = characterStats.MinHitPoints;
            else if (!RangeHelper.IsMaxRangeCorrect(characterStats.MaxHitPoints, amount))
                amount = characterStats.MaxHitPoints;

            characterStats.CurrentHitPoints = amount;
        }

        // Use this to configure the character's hitpoints on start
        public void SetMaxHitpoints(int amount)
        {
            if (!RangeHelper.IsMinRangeCorrect(characterStats.MinHitPoints, amount))
                amount = characterStats.MinHitPoints;

            characterStats.MaxHitPoints = amount;
        }

        #endregion

        // Use this to increment or decrement current hitpoints
        public void SumCurrentHitpoints(int amount)
        {
            characterStats.CurrentHitPoints += amount;

            if (!RangeHelper.IsMinRangeCorrect(characterStats.MinHitPoints, characterStats.CurrentHitPoints))
                characterStats.CurrentHitPoints = characterStats.MinHitPoints;
            else if (!RangeHelper.IsMaxRangeCorrect(characterStats.MaxHitPoints, characterStats.CurrentHitPoints))
                characterStats.CurrentHitPoints = characterStats.MaxHitPoints;
        }

        public void ReturnCurrentToInitial()
        {
            characterStats.CurrentHitPoints = characterStats.InitialHitPoints;

            if (!RangeHelper.IsMinRangeCorrect(characterStats.MinHitPoints, characterStats.CurrentHitPoints))
                characterStats.CurrentHitPoints = characterStats.MinHitPoints;
            else if (!RangeHelper.IsMaxRangeCorrect(characterStats.MaxHitPoints, characterStats.CurrentHitPoints))
                characterStats.CurrentHitPoints = characterStats.MaxHitPoints;
        }

        #region Get Methods

        public int GetCurrentHitPoints() => characterStats.CurrentHitPoints;
        public int GetMaxHitPoints() => characterStats.MaxHitPoints;
        public int GetMinHitPoints() => characterStats.MinHitPoints;

        #endregion

        #endregion

        #region Private Variables

        [SerializeField] private CharacterStats characterStats;

        #endregion
    }
}