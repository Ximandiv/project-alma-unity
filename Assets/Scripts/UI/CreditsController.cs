using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Scripts.UI
{
    public class CreditsController : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private KeyCode keyToSkip = KeyCode.Escape;
        [SerializeField] private float scrollSpeed = 0.6f;
        [SerializeField] private float waitingTime = 2f;
        private bool isScrolling = true;
        private GameObject creditsPanel;
        private RectTransform panelRectTransform;
        private float panelHeight;
        private GameObject returnPanel;

        #endregion

        #region Unity API Methods

        private void Start()
        {
            creditsPanel = gameObject.transform.Find("CreditsPanel").gameObject;
            panelRectTransform = creditsPanel.GetComponent<RectTransform>();
            panelHeight = panelRectTransform.rect.height;

            returnPanel = gameObject.transform.Find("ReturnPanel").gameObject;
        }

        private void Update()
        {
            scrollCredits();

            if (Input.GetKeyDown(keyToSkip))
            {
                scrollSpeed = 4f;
            }
        }

        #endregion

        #region Private Methods

        private void scrollCredits()
        {
            if (isScrolling)
            {
                // Moves the credits panel upwards
                panelRectTransform.anchoredPosition += new Vector2 (0, scrollSpeed + Time.deltaTime);

                //Stops scrolling if the credits panel has moved off the screen.
                if (panelRectTransform.anchoredPosition.y > panelHeight)
                {
                    StartCoroutine(stopScrolling());
                }
            }
        }

        private IEnumerator stopScrolling()
        {
            isScrolling = false;
            returnPanel.SetActive(true);
            yield return new WaitForSeconds(waitingTime);
            returnToMenu();
        }

        private void returnToMenu()
        {
            SceneManager.LoadScene("StartMenu");
        }

        #endregion
    }
}
