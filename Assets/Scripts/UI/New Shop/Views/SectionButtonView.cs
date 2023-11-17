using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionButtonView : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] SectionButtonControl buttonControl;
    [SerializeField] SectionNavigator navigator;

    [Header("View")]
    public Image buttonImage;
    public GameObject selectedSignPrefab;
    GameObject selectedSign;
    public float scaleMultiplier;
    Vector3 initialScale;
    Vector3 multipliedScale;
    public Color normalColor;

    [Header("Floating Panel")]
    public string nameText;
    public string descriptionText;
    public GameObject floatingPanelPrefab;
    [SerializeField] GameObject floatingPanelObj;
    [SerializeField] FloatingPanelShop floatingPanel;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        normalColor = buttonImage.color;

        selectedSign = Instantiate(selectedSignPrefab, transform);
        selectedSign.GetComponent<Image>().color = buttonImage.color;
        selectedSign.SetActive(false);

        initialScale = rect.localScale;
        multipliedScale = rect.localScale * scaleMultiplier;
    }

    void Update()
    {
        UpdateView();
    }
    
    void UpdateView()
    {
        if(buttonControl.Hover && navigator.Control)
        {
            rect.localScale = multipliedScale;

            if(floatingPanelPrefab != null )
            {
                if (floatingPanelObj == null)
                {
                    floatingPanelObj = Instantiate(floatingPanelPrefab, transform);
                    floatingPanel = floatingPanelObj.GetComponent<FloatingPanelShop>();
                    floatingPanel.SetAlternativeMessage("SELECT");
                }

                if (!floatingPanelObj.activeSelf)
                {
                    floatingPanelObj.SetActive(true);
                }

                floatingPanel.SetName(nameText);
                floatingPanel.SetDescription(descriptionText);
                floatingPanel.SetPrice(buttonControl.gearsCost, buttonControl.coresCost, !buttonControl.Purchased);
            }
        }
        else
        {
            rect.localScale = initialScale;

            if (floatingPanelObj != null)
            {
                if (floatingPanelObj.activeSelf) floatingPanelObj.SetActive(false);
            }
        }

        selectedSign.SetActive(buttonControl.Selected);
    }

    public SectionButtonControl GetButtonControl()
    {
        return buttonControl;
    }
}
