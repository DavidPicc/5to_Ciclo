using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionButtonView : MonoBehaviour
{
    [SerializeField] SectionButtonControl buttonControl;
    [SerializeField] SectionNavigator navigator;

    [Header("View")]
    public RectTransform buttonTransform;
    public Image buttonImage;
    Vector2 initialScale;
    public float scaleMultiplier;
    Vector2 multipliedScale;
    public float alphaUnselected;

    [Header("Floating Panel")]
    public string nameText;
    public string descriptionText;
    public GameObject floatingPanelPrefab;
    [SerializeField] GameObject floatingPanelObj;
    [SerializeField] FloatingPanelShop floatingPanel;

    void Start()
    {
        initialScale = buttonTransform.localScale;
        multipliedScale = initialScale * scaleMultiplier;
    }

    void Update()
    {
        UpdateView();
    }
    
    void UpdateView()
    {
        if(buttonControl.Hover && navigator.Control)
        {
            buttonTransform.localScale = multipliedScale;

            if(floatingPanelPrefab != null )
            {
                if (floatingPanelObj == null)
                {
                    floatingPanelObj = Instantiate(floatingPanelPrefab, transform);
                    floatingPanel = floatingPanelObj.GetComponent<FloatingPanelShop>();
                }

                if (!floatingPanelObj.activeSelf)
                {
                    floatingPanelObj.SetActive(true);
                }

                floatingPanel.SetName(nameText);
                floatingPanel.SetDescription(descriptionText);
                floatingPanel.SetPrice(buttonControl.Purchased ? "SELECT" : "Price: " + buttonControl.gearsCost + " gears " + buttonControl.coresCost + " cores");
            }
        }
        else
        {
            buttonTransform.localScale = initialScale;

            if (floatingPanelObj != null)
            {
                if (floatingPanelObj.activeSelf) floatingPanelObj.SetActive(false);
            }
        }

        if(buttonControl.Selected)
        {
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1f);
        }
        else
        {
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, alphaUnselected);
        }


    }

    public SectionButtonControl GetButtonControl()
    {
        return buttonControl;
    }
}
