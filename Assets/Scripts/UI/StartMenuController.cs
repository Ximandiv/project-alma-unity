using Scripts.Events;
using UnityEngine;

namespace Scripts.UI
{
    public class StartMenuController : MonoBehaviour
    {
        public void ChoosePlay()
        {
            GameEvents.Instance.SceneChanged("Village"); //Poner el nombre que va a tener la escena de la vereda
        }

        public void QuitApp()
        {
            Application.Quit();
        }
    }
}
