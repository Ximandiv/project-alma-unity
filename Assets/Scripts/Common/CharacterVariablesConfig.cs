using UnityEngine;

namespace Scripts.Common
{
    /// <summary>
    /// Use this only to configure all max and current variables at once in Start() method
    /// </summary>
    [RequireComponent(typeof(CharacterStatus))]
    [RequireComponent(typeof(CharacterHitpoints))]
    [RequireComponent(typeof(CharacterSpeed))]
    public class CharacterVariablesConfig : MonoBehaviour
    {
        #region Public Methods

        public void ConfigureVariables(float maxSpeed, 
                                        float currSpeed, 
                                        int maxHitpoints, 
                                        int currHitpoints)
        {
            speed.SetMaxSpeed(maxSpeed);
            speed.SetCurrentSpeed(currSpeed);

            hitpoints.SetMaxHitpoints(maxHitpoints);
            hitpoints.SetCurrentHitpoints(currHitpoints);
        }

        #endregion

        #region Private Variables

        [SerializeField] private CharacterStatus status;
        [SerializeField] private CharacterHitpoints hitpoints;
        [SerializeField] private CharacterSpeed speed;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            status = GetComponent<CharacterStatus>();
            hitpoints = GetComponent<CharacterHitpoints>();
            speed = GetComponent<CharacterSpeed>();
        }

        #endregion
    }
}