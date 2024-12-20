using Scripts.Common;
using UnityEngine;
using TMPro;
using System;

namespace Scripts.UI
{
    public class AttemptsUIController : MonoBehaviour
    {
        #region Public Methods

        public void UpdateAttemptsUI()
        {
            if (NPCVariablesAreIncorrect()) return;

            attemptsText.text = $"{NPCAttempts.GetCurrentAttempts()} / {NPCAttempts.GetMaxAttempts()}";
        }

        #endregion


        #region Private Variables

        private TextMeshProUGUI attemptsText;
        private NPCAttemptsController NPCAttempts;

        #endregion


        #region Private Methods

        private void Awake()
        {
            NPCAttempts = GameObject.FindWithTag("NPC").GetComponent<NPCAttemptsController>();
            attemptsText = GameObject.FindWithTag("Attempts").transform
                .Find("AttemptsText")
                .GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            UpdateAttemptsUI();
        }

        private bool NPCVariablesAreIncorrect()
        {
            return (NPCAttempts.GetCurrentAttempts() < 0 || NPCAttempts.GetCurrentAttempts() > NPCAttempts.GetMaxAttempts() || NPCAttempts.GetCurrentAttempts() < NPCAttempts.GetMinAttempts());
        }

        #endregion
    }
}