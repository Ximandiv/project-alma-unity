using UnityEngine;
using Scripts.Scriptables;
using TMPro;

namespace Scripts.UI
{
    public class PowerupPopupController : MonoBehaviour
    {
        #region Public Methods

        public void OpenPowerupPopup(string powerupName, string powerupDescription)
        {
            gameStatus.Pause();
            powerupPopup.SetActive(true);
            displayedName.text = powerupName;
            displayedDescription.text = powerupDescription;

        }

        public void ClosePowerupPopup()
        {
            gameStatus.Unpause();
            powerupPopup.SetActive(false);
        }

        #endregion

        #region Private Variables

        [SerializeField] private GameStatus gameStatus;
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
