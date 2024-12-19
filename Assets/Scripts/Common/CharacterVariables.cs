using UnityEngine;

namespace Scripts.Common
{
    public class CharacterVariables : MonoBehaviour
    {
        #region Public Methods

        #region Set Methods

        public void SetCurrentHitpoints(int amount)
        {
            if(amount < minHitPoints)
                amount = minHitPoints;
            else if(amount > maxHitPoints)
                amount = maxHitPoints;

            currentHitpoints = amount;
        }

        public void SetCurrentSpeed(float amount)
        {
            if (amount < minSpeed)
                amount = minSpeed;
            else if (amount > maxSpeed)
                amount = maxSpeed;

            currentSpeed = amount;
        }

        public void SetMovingStatus() => isMoving = !isMoving;

        #endregion

        #region Get Methods

        public int GetCurrentHitpoints() => currentHitpoints;
        public int GetMaxHitPoints() => maxHitPoints;
        public float GetCurrentSpeed() => currentSpeed;
        public bool IsMoving() => isMoving;

        #endregion

        #endregion

        #region Private Variables

        [Header("Status")]

        [SerializeField] private bool isMoving = false;

        [Header("Current Variables")]

        [SerializeField] private int currentHitpoints = 2;
        [SerializeField] private float currentSpeed = 5f;

        [Header("Range of Variables")]

        [SerializeField] private int minHitPoints = 1;
        [SerializeField] private int maxHitPoints = 2;

        [SerializeField] private float minSpeed = 1f;
        [SerializeField] private float maxSpeed = 10f;

        #endregion
    }
}
