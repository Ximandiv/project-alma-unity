using UnityEngine;
using System.Collections;

namespace Scripts.UI
{
    public class InGameHelpController : MonoBehaviour
    {
        #region Public Methods

        public void OpenVillageHelp()
        {
            villageHelpPanel.SetActive(true);
            StartCoroutine(waitBeforeClosing());
        }

        public void OpenMindHelp()
        {
            mindHelpPanel.SetActive(true);
            StartCoroutine(waitBeforeClosing());
        }

        #endregion

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
