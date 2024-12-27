using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptables/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public Message[] Messages;
        public Actor[] Actors;

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
    }
}
