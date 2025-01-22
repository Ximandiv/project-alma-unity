using UnityEngine;
using System.Collections;
using Scripts.Events;

namespace Scripts.UI
{
    public class InGameHelpController : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private float helpTime = 6f;
        private GameObject villageHelpPanel;
        private GameObject mindHelpPanel;
        private bool canSkipHelp = false;

        #endregion

        #region Unity API Methods

        private void Awake()
        {
            villageHelpPanel = gameObject.transform.Find("VillageHelpPanel").gameObject;
            mindHelpPanel = gameObject.transform.Find("MindHelpPanel").gameObject;

            villageHelpPanel.SetActive(false);
            mindHelpPanel.SetActive(false);

            canSkipHelp = false;
        }

        private void OnEnable()
        {
            GameEvents.Instance.OnGameStarted += openVillageHelp;
            GameEvents.Instance.OnLevelStarted += openMindHelp;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnGameStarted -= openVillageHelp;
            GameEvents.Instance.OnLevelStarted -= openMindHelp;
        }

        void Update()
        {
            if (canSkipHelp)
            {
                if (Input.anyKeyDown)
                {
                    closeHelp();
                }
            }

        }

        #endregion

        #region Private Methods

        private void openVillageHelp()
        {
            villageHelpPanel.SetActive(true);
            StartCoroutine(waitBeforeClosing());
        }

        private void openMindHelp()
        {
            mindHelpPanel.SetActive(true);
            StartCoroutine(waitBeforeClosing());
        }

        private IEnumerator waitBeforeClosing()
        {
            yield return new WaitForSeconds(helpTime);
            canSkipHelp = true;
        }

        private void closeHelp()
        {
            villageHelpPanel.SetActive(false);
            mindHelpPanel.SetActive(false);
            canSkipHelp = false;
        }

        #endregion
    }
}
