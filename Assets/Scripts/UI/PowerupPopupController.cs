using UnityEngine;
using Scripts.Events;
using TMPro;

namespace Scripts.UI
{
    public class PowerupPopupController : MonoBehaviour
    {
        #region Public Methods

        public void ClosePowerupPopup()
        {
            GameEvents.Instance.Unpaused();
            powerupPopupPanel.SetActive(false);
        }

        #endregion

        #region Private Variables

        [SerializeField] private GameObject powerupPopupPanel;
        [SerializeField] private TextMeshProUGUI displayedName;
        [SerializeField] private TextMeshProUGUI displayedDescription;
        
        #endregion

        #region Unity API Methods

        private void Awake()
        {
            powerupPopupPanel.SetActive(false);
        }

        private void OnEnable()
        {
            GameEvents.Instance.OnPowerupObtained += openPowerupPopup;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnPowerupObtained -= openPowerupPopup;
        }

        #endregion

        #region Private Methods

        private void openPowerupPopup(string powerupName, string powerupDescription)
        {
            GameEvents.Instance.Paused();
            powerupPopupPanel.SetActive(true);
            displayedName.text = powerupName;
            displayedDescription.text = powerupDescription;

        }

        #endregion
    }
}
