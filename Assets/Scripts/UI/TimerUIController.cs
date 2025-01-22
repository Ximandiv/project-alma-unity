using UnityEngine;
using Scripts.Events;
using TMPro;

namespace Scripts.UI
{
    public class TimerUIController : MonoBehaviour
    {
        private TextMeshProUGUI timerText;

        private void Awake()
        {
            timerText = gameObject.transform.Find("TimerText").GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            GameEvents.Instance.OnTimerUpdated += UpdateTimerText;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnTimerUpdated -= UpdateTimerText;
        }

        public void UpdateTimerText(float remainingTime)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}