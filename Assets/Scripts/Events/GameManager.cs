using UnityEngine;
using Scripts.Scriptables;
using System;

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
        gameStatus.PauseCount++;
            if (gameStatus.PauseCount == 1)
            {
                gameStatus.IsPaused = true;
                Time.timeScale = 0f;
            }
    }

    private void handleUnpause()
    {
        gameStatus.PauseCount--;
            if (gameStatus.PauseCount == 0)
            {
                gameStatus.IsPaused = false;
                Time.timeScale = 1f;
            }
    }
}
