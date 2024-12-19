using Scripts.Common;
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
            float fillWidth = maxWidth * ((float)characterVariables.GetCurrentHitpoints() / (float)characterVariables.GetMaxHitPoints());

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
        private CharacterVariables characterVariables;

        #endregion


        #region Private Methods

        private void Awake()
        {
            characterVariables = GameObject.FindWithTag("Player").GetComponent<CharacterVariables>();
            healthBar = GameObject.FindWithTag("HealthBar").GetComponent<RectTransform>();
        }

        private void Start()
        {
            fillBar = healthBar.transform
                .Find("Background").transform
                .Find("Fill")
                .GetComponent<RectTransform>();
            UpdateHealthBar();
        }

        private Boolean characterVariablesAreIncorrect()
        {
            return (characterVariables.GetCurrentHitpoints() < 0 || characterVariables.GetCurrentHitpoints() > characterVariables.GetMaxHitPoints() || characterVariables.GetCurrentHitpoints() < characterVariables.GetMinHitPoints());
        }

        #endregion
    }
}
