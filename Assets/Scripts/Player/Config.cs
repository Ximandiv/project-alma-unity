using Scripts.Common;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(CharacterVariablesConfig))]
    public class Config: MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private CharacterVariablesConfig variablesConfig;

        [SerializeField] private int maxHitpoints = 10;
        [SerializeField] private int currHitPoints = 10;
        [SerializeField] private float maxSpeed = 12f;
        [SerializeField] private float currSpeed = 8f;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            config = GetComponent<CharacterVariablesConfig>();
        }

        private void Start()
        {
            config.ConfigureVariables(maxSpeed,
                                    currSpeed,
                                    maxHitpoints,
                                    currHitPoints);
        }

        #endregion
    }
}