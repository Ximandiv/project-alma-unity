using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class MenuController : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitApp()
        {
            Application.Quit();
        }
    }
}

