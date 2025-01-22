using Scripts.Events;
using UnityEngine;

namespace Scripts.Events
{
    public class SceneLoadTrigger : MonoBehaviour
    {
        public void ChangeScene(string sceneName) => GameEvents.Instance.SceneChanged(sceneName);
    }
}
