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
            if (!RangeHelper.IsMinRangeCorrect(minSpeed, amount))
                amount = minSpeed;
            else if (!RangeHelper.IsMaxRangeCorrect(maxSpeed, amount))
                amount = maxSpeed;

            currentSpeed = amount;
        }

        public void SetMaxSpeed(float amount)
        {
            if (!RangeHelper.IsMinRangeCorrect(minSpeed, amount))
                amount = minSpeed;

            maxSpeed = amount;
        }

        #endregion

        // Use this to increment or decrement current speed
        public void SumCurrentSpeed(float amount)
        {
            currentSpeed += amount;

            if (!RangeHelper.IsMinRangeCorrect(minSpeed, currentSpeed))
                currentSpeed = minSpeed;
            else if (!RangeHelper.IsMaxRangeCorrect(maxSpeed, currentSpeed))
                currentSpeed = maxSpeed;
        }
        public float GetCurrentSpeed() => currentSpeed;

        #endregion

        #region Private Variables

        [SerializeField] private float currentSpeed = 5f;

        [SerializeField] private float minSpeed = 1f;
        [SerializeField] private float maxSpeed = 10f;

        #endregion
    }
}