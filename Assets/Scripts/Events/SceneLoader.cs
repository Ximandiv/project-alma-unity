using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Scripts.Scriptables;

namespace Scripts.Events
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameStatus gameStatus;
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
            gameStatus?.Pause();

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(sceneName);
            gameStatus?.Unpause();
        }
    }
}