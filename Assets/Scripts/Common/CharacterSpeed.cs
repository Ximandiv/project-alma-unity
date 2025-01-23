using Scripts.Scriptables;
using UnityEngine;

namespace Scripts.Common
{
    public class CharacterSpeed : MonoBehaviour
    {
        #region Public Methods

        #region Set Methods

        // Use this to configure the character's speed on start
        public void SetCurrentSpeed(float amount)
        {
            if (!RangeHelper.IsMinRangeCorrect(characterStats.MinSpeed, amount))
                amount = characterStats.MinSpeed;
            else if (!RangeHelper.IsMaxRangeCorrect(characterStats.MaxSpeed, amount))
                amount = characterStats.MaxSpeed;

            characterStats.CurrentSpeed = amount;
        }

        public void SetMaxSpeed(float amount)
        {
            if (!RangeHelper.IsMinRangeCorrect(characterStats.MinSpeed, amount))
                amount = characterStats.MinSpeed;

            characterStats.MaxSpeed = amount;
        }

        #endregion

        // Use this to increment or decrement current speed
        public void SumCurrentSpeed(float amount)
        {
            characterStats.CurrentSpeed += amount;

            if (!RangeHelper.IsMinRangeCorrect(characterStats.MinSpeed, characterStats.CurrentSpeed))
                characterStats.CurrentSpeed = characterStats.MinSpeed;
            else if (!RangeHelper.IsMaxRangeCorrect(characterStats.MaxSpeed, characterStats.CurrentSpeed))
                characterStats.CurrentSpeed = characterStats.MaxSpeed;
        }

        public void ReturnCurrentToInitial()
        {
            characterStats.CurrentSpeed = characterStats.InitialSpeed;

            if (!RangeHelper.IsMinRangeCorrect(characterStats.MinSpeed, characterStats.CurrentSpeed))
                characterStats.CurrentSpeed = characterStats.MinSpeed;
            else if (!RangeHelper.IsMaxRangeCorrect(characterStats.MaxSpeed, characterStats.CurrentSpeed))
                characterStats.CurrentSpeed = characterStats.MaxSpeed;
        }

        #region Get Methods

        public float GetCurrentSpeed() => characterStats.CurrentSpeed;

        #endregion

        #endregion

        #region Private Variables

        [SerializeField] private CharacterStats characterStats;

        #endregion
    }
}