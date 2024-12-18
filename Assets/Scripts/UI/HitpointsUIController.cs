using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class HitpointsUIController : MonoBehaviour
{
    // preguntar si la carpeta UI/NPCMindLevel es correcta y el orden en la Hierarchy

    public void RegainLife(float amountToRegain)
    {
        currentLife = Mathf.Min(currentLife + amountToRegain, totalLife); // Ensure life doesn't exceed max hearts
        UpdateHeartsDisplay();
    }

    public void LoseLife(float amountToLose)
    {
        currentLife = Mathf.Max(currentLife - amountToLose, 0); // Ensure life doesn't go below 0
        UpdateHeartsDisplay();
    }

    //preguntar si este metodo si es buena idea
    public void UpgradeTotalLife(float newTotalLife)
    {
        totalLife = newTotalLife;
        UpdateHeartsDisplay();
    }

    //preguntar si esta correcta la estructura y roden de las variables y metodos (primero lo publico y despues lo privado) , pero no estoy segunro donde debe ir ubicado las funciones como el Awake, Start y Update
    [SerializeField] private Canvas CanvasNPCMindLevel;
    [SerializeField] private Image SpriteHeart;
    [SerializeField] private int AmountSpriteHeart = 3;

    private Image[] heartImages; 

    private float totalLife;
    private float currentLife;

    void Awake()
    {
        totalLife = 6f; // FIX: pregutnar como obtener la vida totyal directametne desde el personaje
        currentLife = totalLife;
    }

    void Start()
    {
        heartImages = new Image[AmountSpriteHeart];

        for (int i = 0; i < AmountSpriteHeart; i++)
        {
            Image newHeart = Instantiate(SpriteHeart, CanvasNPCMindLevel.transform);

            float xOffset = -i * 35 - 10;
            newHeart.rectTransform.anchoredPosition = new Vector2(xOffset, -10);

            heartImages[i] = newHeart;
        }
    }

    private void UpdateHeartsDisplay()
    {
        float lifePerHeart = totalLife / AmountSpriteHeart;

        for (int i = 0; i < AmountSpriteHeart; i++)
        {
            float lifeForThisHeart = Mathf.Clamp(currentLife - (i * lifePerHeart), 0, lifePerHeart);

            heartImages[i].fillAmount = lifeForThisHeart / lifePerHeart;
        }
    }






    //Testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LoseLife(1f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            RegainLife(1f);
        }
    }
}
