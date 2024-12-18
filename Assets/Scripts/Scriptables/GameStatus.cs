using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Game Status", menuName = "Scriptables/GameStatus")]
    public class GameStatus : ScriptableObject
    {
        public bool IsGamePaused;
    }
}
