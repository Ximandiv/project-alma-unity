using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Game Status", menuName = "Scriptables/GameStatus")]
    public class GameStatus : ScriptableObject
    {
        public bool IsPaused = false;
        public int PauseCount = 0;

        private void OnEnable()
        {
            IsPaused = false;
            PauseCount = 0;
        }

    }
}
