using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionButtonView : MonoBehaviour
{
    [SerializeField] SectionButtonControl buttonControl;
    [SerializeField] SectionNavigator navigator;

    [Header("View")]
    public Image buttonImage;
    public GameObject selectedSignPrefab;
    GameObject selectedSign;

    [Header("Floating Panel")]
    public string nameText;
    public string descriptionText;
    public GameObject floatingPanelPrefab;
    [SerializeField] GameObject floatingPanelObj;
    [SerializeField] FloatingPanelShop floatingPanel;

    void Start()
    {
        selectedSign = Instantiate(selectedSignPrefab, transform);
        selectedSign.GetComponent<Image>().color = buttonImage.color;
        selectedSign.SetActive(false);
    }

    void Update()
    {
        UpdateView();
    }
    
    void UpdateView()
    {
        if(buttonControl.Hover && navigator.Control)
        {
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
