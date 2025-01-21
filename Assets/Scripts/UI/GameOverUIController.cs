using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Scripts.Events;

namespace Scripts.UI
{
    public class GameOverUIController : MonoBehaviour
{
    public void OpenGameOverScreen()
    {
        GameEvents.Instance.Paused();
        gameOverPanel.SetActive(true);
        StartCoroutine(returnToMenu());
    }
    
    [SerializeField] private float gameOverScreenTime = 3f;
    private GameObject gameOverPanel;

    private void Awake()
    {
        gameOverPanel = gameObject.transform.Find("GameOverPanel").gameObject;
        gameOverPanel.SetActive(false);
    }

    private IEnumerator returnToMenu()
    {
        yield return new WaitForSeconds(gameOverScreenTime);
        GameEvents.Instance.Unpaused();
        SceneManager.LoadScene("StartMenu");
    }

}
}
