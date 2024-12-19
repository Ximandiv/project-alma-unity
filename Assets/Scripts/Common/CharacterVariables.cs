using UnityEngine;

namespace Scripts.Common
{
    public class CharacterVariables : MonoBehaviour
    {
        #region Public Methods

        #region Set Methods

        // Use this to configure the character's hitpoints on start
        public void SetCurrentHitpoints(int amount)
        {
            if(!isMinRangeCorrect(minHitPoints, amount))
                amount = minHitPoints;
            else if(!isMaxRangeCorrect(maxHitPoints, amount))
                amount = maxHitPoints;

            currentHitpoints = amount;
        }

        // Use this to increment or decrement current hitpoints
        public void SumCurrentHitpoints(int amount)
        {
            currentHitpoints += amount;

            if (!isMinRangeCorrect(minHitPoints, currentHitpoints))
                currentHitpoints = minHitPoints;
            else if(!isMaxRangeCorrect(maxHitPoints, currentHitpoints))
                currentHitpoints = maxHitPoints;
        }

        // Use this to configure the character's speed on start
        public void SetCurrentSpeed(float amount)
        {
            if (!isMinRangeCorrect((int)minSpeed, (int)amount))
                amount = minSpeed;
            else if (!isMaxRangeCorrect((int)maxSpeed, (int)amount))
                amount = maxSpeed;

            currentSpeed = amount;
        }

        // Use this to increment or decrement current speed
        public void SumCurrentSpeed(float amount)
        {
            currentSpeed += amount;

            if (!isMinRangeCorrect((int)minSpeed, (int)currentSpeed))
                currentSpeed = minSpeed;
            else if (!isMaxRangeCorrect((int)maxSpeed, (int)currentSpeed))
                currentSpeed = maxSpeed;
        }

        public void SetMovingStatus(bool status) => isMoving = status;
        public void SetCanMoveStatus(bool status) => canMove = status;

        #endregion

        #region Get Methods

        public int GetCurrentHitpoints() => currentHitpoints;
        public int GetMaxHitPoints() => maxHitPoints;
        public int GetMinHitPoints() => minHitPoints;
        
        public float GetCurrentSpeed() => currentSpeed;
        public bool IsMoving() => isMoving;
        public bool IsCapableOfMovement() => canMove;

        #endregion

        #endregion

        #region Private Variables

        [Header("Status")]

        [SerializeField] private bool isMoving = false;
        [SerializeField] private bool canMove = true;

        [Header("Current Variables")]

        [SerializeField] private int currentHitpoints = 2;
        [SerializeField] private float currentSpeed = 5f;

        [Header("Range of Variables")]

        [SerializeField] private int minHitPoints = 1;
        [SerializeField] private int maxHitPoints = 2;

        [SerializeField] private float minSpeed = 1f;
        [SerializeField] private float maxSpeed = 10f;

        #endregion

        #region Private Methods

        private bool isMinRangeCorrect(int minValue, int value) => value > minValue;

        private bool isMaxRangeCorrect(int maxValue, int value) => value < maxValue;

        #endregion
    }
}
