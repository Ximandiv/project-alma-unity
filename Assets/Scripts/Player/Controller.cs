using Scripts.Common;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(CharacterVariablesConfig))]
    [RequireComponent(typeof(CharacterSpeed))]
    [RequireComponent(typeof(CharacterHitpoints))]
    [RequireComponent(typeof(CharacterStatus))]
    public class Controller : MonoBehaviour
    {
        #region Public Methods
        public void SumHitpoints(int amount) => hitpoints.SumCurrentHitpoints(amount);
        public void SumSpeed(int amount) => speed.SumCurrentSpeed(amount);
        public void SetMovingStatus(bool newStatus) => status.SetMovingStatus(newStatus);
        public void SetCanMoveStatus(bool newStatus) => status.SetCanMoveStatus(newStatus);

        public float GetCurrentSpeed() => speed.GetCurrentSpeed();
        public bool GetIsMoving() => status.IsMoving();
        public bool GetCanMove() => status.IsCapableOfMovement();

        #endregion

        #region Private Variables

        [SerializeField] private CharacterVariablesConfig config;
        
        [SerializeField] private CharacterSpeed speed;
        [SerializeField] private CharacterHitpoints hitpoints;
        [SerializeField] private CharacterStatus status;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            config = GetComponent<CharacterVariablesConfig>();
            speed = GetComponent<CharacterSpeed>();
            hitpoints = GetComponent<CharacterHitpoints>();
            status = GetComponent<CharacterStatus>();
        }

        private void Start()
        {
            int maxHitpoints = 10;
            int currHitPoints = 10;
            float maxSpeed = 12f;
            float currSpeed = 8f;

            config.ConfigureVariables(maxSpeed,
                                    currSpeed,
                                    maxHitpoints,
                                    currHitPoints);
        }

        #endregion
    }
}