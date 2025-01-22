using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Scripts.Events
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Animator transition;
        [SerializeField] private float transitionTime = 1f;

        private void Awake()
        {
            transition.gameObject.SetActive(true);
        }

        private void Start()
        {
            GameEvents.Instance.OnSceneChanged += changeScene;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnSceneChanged -= changeScene;
        }

        private void changeScene(string sceneName)
        {
            StartCoroutine(sceneTransition(sceneName));
        }

        private IEnumerator sceneTransition(string sceneName)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(sceneName);
        }
    }
}