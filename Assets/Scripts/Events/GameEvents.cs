using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;
    public event Action OnPause;
    public event Action OnUnpause;

     private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    public void Pause()
    {
        OnPause?.Invoke();
    }

    public void Unpause()
    {
        OnUnpause?.Invoke();
    }
}
