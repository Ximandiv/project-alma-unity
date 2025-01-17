using Scripts.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private MenuController menuController;
    private DialogueController dialogueController;

    private void Awake()
    {
        menuController = FindFirstObjectByType<MenuController>();
        dialogueController = FindFirstObjectByType<DialogueController>();
    }

    private void Start()
    {
        GameEvents.Instance.OnMenuToggle += handleMenuToggle;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnMenuToggle -= handleMenuToggle;
    }

    private void handleMenuToggle()
    {
        if(!menuController.IsOpen)
        {
            menuController.OpenMenu();
            if (dialogueController.IsOpen) dialogueController.DisableDialogue();
        }
        else 
        {
            menuController.CloseMenu();
            if (dialogueController.IsOpen) dialogueController.EnableDialogue();
        }
    }

}
