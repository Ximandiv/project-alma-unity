using Scripts.Common;
using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class HitpointsUIController : MonoBehaviour
{

    #region Public Methods

    public void UpdateHealthBar()
    {
        // Get the maximum size of the bar (width of the bar bottom)
        float maxWidth = healthBar.GetComponent<RectTransform>().sizeDelta.x;

        // Calculate the current life ratio and set the width of the fill bar
        float fillWidth = maxWidth * ((float)characterVariables.GetCurrentHitpoints() / characterVariables.GetMaxHitPoints());

        // Update the size of the fill bar
        fillBar.sizeDelta = new Vector2(fillWidth, fillBar.sizeDelta.y);
    }

    public void EnlargeHealthBar()
    {
        RectTransform rectTransform = healthBar.GetComponent<RectTransform>();
        rectTransform.sizeDelta += new Vector2(10f, 0); // Increase width only
    }

    #endregion


    #region Private Variables

    [SerializeField] private GameObject healthBar;
    
    private RectTransform fillBar;
    private CharacterVariables characterVariables;

    #endregion


    #region Private Methods

    private void Start()
    {
        characterVariables = GameObject.FindWithTag("Player").GetComponent<CharacterVariables>();
        fillBar = healthBar.transform
            .Find("Background").transform
            .Find("Fill")
            .GetComponent<RectTransform>();
        UpdateHealthBar();
    }

    #endregion
}
