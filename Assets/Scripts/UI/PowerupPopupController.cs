using UnityEngine;
using Scripts.Events;
using TMPro;

namespace Scripts.UI
{
    public class PowerupPopupController : MonoBehaviour
    {
        #region Public Methods

        public void OpenPowerupPopup(string powerupName, string powerupDescription)
        {
            GameEvents.Instance.Pause();
            powerupPopup.SetActive(true);
            displayedName.text = powerupName;
            displayedDescription.text = powerupDescription;

        }

        public void ClosePowerupPopup()
        {
            GameEvents.Instance.Unpause();
            powerupPopup.SetActive(false);
        }

        #endregion

        #region Private Variables

        [SerializeField] private GameObject powerupPopup;
        [SerializeField] private TextMeshProUGUI displayedName;
        [SerializeField] private TextMeshProUGUI displayedDescription;
        
        #endregion

        #region Unity API Methods

        private void Awake()
        {
            powerupPopup.SetActive(false);
        }

        #endregion
    }
}
