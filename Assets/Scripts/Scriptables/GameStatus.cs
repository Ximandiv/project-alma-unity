using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Game Status", menuName = "Scriptables/GameStatus")]
    public class GameStatus : ScriptableObject
    {
        public bool IsPaused
        {
            get { return isPaused; }
        }
        
        public void Pause()
        {
            pauseCount++;
            if (pauseCount == 1)
            {
                isPaused = true;
            }
        }

        public void Unpause()
        {
            pauseCount--;
            if (pauseCount == 0)
            {
                isPaused = false;
            }
        }

        private bool isPaused = false;
        private int pauseCount = 0;

        private void OnEnable()
        {
            isPaused = false;
            pauseCount = 0;
        }

    }
}
