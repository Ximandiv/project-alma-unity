using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void changeScene(string sceneName)
    {
        gameObject.SetActive(true);
        StartCoroutine(sceneTransition(sceneName));
    }

    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    private IEnumerator sceneTransition(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
