using UnityEngine;
using Scripts.Scriptables;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStatus gameStatus;

    private void Start()
    {
        GameEvents.Instance.OnPause += handlePause;
        GameEvents.Instance.OnUnpause += handleUnpause;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnPause -= handlePause;
        GameEvents.Instance.OnUnpause -= handleUnpause;
    }

    private void handlePause()
    {
        gameStatus.Pause();
    }

    private void handleUnpause()
    {
        gameStatus.Unpause();
    }
}
