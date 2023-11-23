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

    public GameObject framePrefab;
    GameObject frame;
    public Image frameImage;

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

        frame = Instantiate(framePrefab, transform);
        frameImage = frame.GetComponent<Image>();

        if (buttonControl.Purchased)
        {
            frameImage.fillAmount = 1f;
        }
        else
        {
            frameImage.fillAmount = 0f;
        }

        frameImage.color = normalColor;

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
            selectedSign.SetActive(true);
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
            selectedSign.SetActive(false);
            rect.localScale = initialScale;

            if (floatingPanelObj != null)
            {
                if (floatingPanelObj.activeSelf) floatingPanelObj.SetActive(false);
            }
        }

        frameImage.color = new Color(frameImage.color.r, frameImage.color.g, frameImage.color.b, !buttonControl.Purchased ? 0.1f : 0.4f);

        //selectedSign.SetActive(buttonControl.Selected);
    }

    public SectionButtonControl GetButtonControl()
    {
        return buttonControl;
    }
}
