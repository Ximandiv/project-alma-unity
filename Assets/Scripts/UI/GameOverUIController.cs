using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Scripts.Events;

public class GameOverUIController : MonoBehaviour
{
    public void OpenGameOverScreen()
    {
        GameEvents.Instance.Paused();
        GameOverPanel.SetActive(true);
        StartCoroutine(returnToMenu());
    }
    
    [SerializeField] private float waitingTime = 3f;
    private GameObject GameOverPanel;

    private void Awake()
    {
        GameOverPanel = gameObject.transform.Find("GameOverPanel").gameObject;
    }

    private IEnumerator returnToMenu()
    {
        yield return new WaitForSeconds(waitingTime);
        GameEvents.Instance.Unpaused();
        SceneManager.LoadScene("StartMenu");
    }

}
