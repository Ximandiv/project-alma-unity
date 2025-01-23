using Scripts.Common;
using Scripts.Player;
using System;
using UnityEngine;

namespace Scripts.UI
{
    public class HitpointsUIController : MonoBehaviour
    {
        #region Public Methods

        public void UpdateHealthBar()
        {
            if (characterVariablesAreIncorrect()) return;

            // Get the maximum size of the bar (width of the bar bottom)
            float maxWidth = healthBar.sizeDelta.x;

            // Calculate the current life ratio and set the width of the fill bar
            float fillWidth = maxWidth * ((float)characterHitpoints.GetCurrentHitPoints() / (float)characterHitpoints.GetMaxHitPoints());

            // Update the size of the fill bar
            fillBar.sizeDelta = new Vector2(fillWidth, fillBar.sizeDelta.y);
        }

        public void EnlargeHealthBar()
        {
            healthBar.sizeDelta += new Vector2(10f, 0); // Increase width only
            UpdateHealthBar();
        }

        #endregion


        #region Private Variables

        private RectTransform healthBar;
        private RectTransform fillBar;
        private CharacterHitpoints characterHitpoints;
        private GameObject player;
        private Health playerHealth;

        #endregion


        #region Private Methods

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            characterHitpoints = player.GetComponent<CharacterHitpoints>();
            playerHealth = player.GetComponent<Health>();
            healthBar = gameObject.transform.Find("HealthBar").GetComponent<RectTransform>();
            fillBar = gameObject.transform.Find("HealthBar/Fill").GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            player.GetComponent<Health>().OnPlayerDamaged += UpdateHealthBar;
        }

        private void Start()
        {
            UpdateHealthBar();
        }

        private void OnDestroy()
        {
            playerHealth.OnPlayerDamaged -= UpdateHealthBar;
        }

        private bool characterVariablesAreIncorrect()
        {
            return (characterHitpoints.GetCurrentHitPoints() < 0 || characterHitpoints.GetCurrentHitPoints() > characterHitpoints.GetMaxHitPoints() || characterHitpoints.GetCurrentHitPoints() < characterHitpoints.GetMinHitPoints());
        }

        #endregion
    }
}
