using UnityEngine;
using System.Collections;
using Scripts.Events;

namespace Scripts.UI
{
    public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private float gameOverScreenTime = 2f;
    private GameObject gameOverPanel;

    private void Awake()
    {
        gameOverPanel = gameObject.transform.Find("GameOverPanel").gameObject;
        gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnGameOver += openGameOverScreen;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameOver -= openGameOverScreen;
    }

    private void openGameOverScreen()
    {
        GameEvents.Instance.Paused();
        gameOverPanel.SetActive(true);
        StartCoroutine(returnToMenu());
    }

    private IEnumerator returnToMenu()
    {
        yield return new WaitForSeconds(gameOverScreenTime);
        GameEvents.Instance.Unpaused();
        GameEvents.Instance.SceneChanged("StartMenu");
    }

}
}
