using Scripts.Events;
using UnityEngine;

namespace Scripts.UI
{
    public class StartMenuController : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            GameEvents.Instance.SceneChanged(sceneName);
        }

        public void QuitApp()
        {
            Application.Quit();
        }
    }
}
