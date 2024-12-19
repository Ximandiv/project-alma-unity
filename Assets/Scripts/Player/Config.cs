using Scripts.Common;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(CharacterConfig))]
    public class Config: MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private CharacterConfig config;

        [SerializeField] private int maxHitpoints = 10;
        [SerializeField] private int currHitPoints = 10;
        [SerializeField] private float maxSpeed = 12f;
        [SerializeField] private float currSpeed = 8f;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            config = GetComponent<CharacterConfig>();
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