using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;
    public event Action OnPause;
    public event Action OnUnpause;
    public event Action OnMenuToggle;

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

    public void MenuToggle()
    {
        OnMenuToggle?.Invoke();
    }
}
