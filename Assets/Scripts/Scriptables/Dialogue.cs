using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptables/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        #region Public Variables

        public Message[] Messages;
        public Actor[] Actors;
        public bool IsLevelStarter = false;

        #endregion

        #region Public Classes

        [System.Serializable]
        public class Message
        {
            [SerializeField] private int actorId;
            [SerializeField] private string messageText;

            public int ActorId => actorId;
            public string MessageText => messageText;
        }

        [System.Serializable]
        public class Actor
        {
            [SerializeField] private string actorName;
            [SerializeField] private Sprite actorSprite;

            public string ActorName => actorName;
            public Sprite ActorSprite => actorSprite;
        }

        #endregion
    }
}
