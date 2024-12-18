using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Game Status", menuName = "Scriptables/GameStatus")]
    public class GameStatus : ScriptableObject
    {
        public void Pause() => IsPaused = !IsPaused;

        public bool IsPaused = false;
    }
}
