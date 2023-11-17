using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    private const float OPACITY_PURCHASED = .2f;
    private const float OPACITY_NOT_PURCHASED = .6f;

    RectTransform rect;
    [SerializeField] SectionButtonControl sectionButtonControl;
    [SerializeField] UpgradeController upgradeControl;

    [Header("View")]
    public Image upgradeImage;
    public GameObject lockedSignPrefab;
    public GameObject selectSignPrefab;
    public GameObject framePrefab;
    GameObject lockedSign;
    GameObject selectSign;
    GameObject frame;
    Image frameImage;
    public float scaleMultiplier;
    Vector3 initialScale;
    Vector3 multipliedScale;
    public Color normalColor;
    public Color lockedColor = Color.grey;


    [Header("Floating Panel")]
    public string nameText;
    public string descriptionText;
    public GameObject floatingPanelPrefab;
    [SerializeField] GameObject floatingPanelObj;
    [SerializeField] FloatingPanelShop floatingPanel;

    void Start()
    {
        sectionButtonControl = FindObjectOfType<SectionNavigator>().hoverButton;

        normalColor = GetComponent<Image>().color;
        Debug.Log(name + "    " + normalColor);
        rect = GetComponent<RectTransform>();

        lockedSign = Instantiate(lockedSignPrefab, transform);
        //lockedSign.GetComponent<Image>().color = upgradeImage.color;
        lockedSign.SetActive(false);

        selectSign = Instantiate(selectSignPrefab, transform);
        //selectSign.GetComponent<Image>().color = upgradeImage.color;
        selectSign.SetActive(false);

        frame = Instantiate(framePrefab, transform);
        frameImage = frame.GetComponent<Image>();
        //frameImage.color = upgradeImage.color;

        if (upgradeControl.Locked)
        {
            ChangeIconColor(lockedColor);
        }
        else
        {
            ChangeIconColor(normalColor);
        }

        initialScale = rect.localScale;
        multipliedScale = rect.localScale * scaleMultiplier;
    }

    void Update()
    {
        UpdateView();
    }

    void UpdateView()
    {
        if (upgradeControl.Selected)
        {
            selectSign.SetActive(true);

            if (floatingPanelPrefab != null)
            {
                if (floatingPanelObj == null)
                {
                    floatingPanelObj = Instantiate(floatingPanelPrefab, transform);
                    floatingPanel = floatingPanelObj.GetComponent<FloatingPanelShop>();
                    floatingPanel.SetAlternativeMessage("");
                }

                if (!floatingPanelObj.activeSelf)
                {
                    floatingPanelObj.SetActive(true);
                }

                floatingPanel.SetName(nameText);
                floatingPanel.SetDescription(descriptionText);
                floatingPanel.SetPrice(upgradeControl.gearsCost, upgradeControl.coresCost, !upgradeControl.Purchased);
            }

            rect.localScale = multipliedScale;
        }
        else
        {
            selectSign.SetActive(false);

            if (floatingPanelObj != null)
            {
                if (floatingPanelObj.activeSelf) floatingPanelObj.SetActive(false);
            }

            rect.localScale = initialScale;
        }

        lockedSign.SetActive(upgradeControl.Locked);
        frameImage.color = new Color(frameImage.color.r, frameImage.color.g, frameImage.color.b, !upgradeControl.Purchased ? OPACITY_PURCHASED : OPACITY_NOT_PURCHASED);
        upgradeImage.color = new Color(upgradeImage.color.r, upgradeImage.color.g, upgradeImage.color.b, !upgradeControl.Locked ? OPACITY_NOT_PURCHASED : OPACITY_PURCHASED);
        selectSign.GetComponent<Image>().color = new Color(sectionButtonControl.GetComponent<Image>().color.r, sectionButtonControl.GetComponent<Image>().color.g,
            sectionButtonControl.GetComponent<Image>().color.b, sectionButtonControl.GetComponent<Image>().color.a);
    }

    public UpgradeController GetUpgradeControl()
    {
        return upgradeControl;
    }

    public void ChangeIconColor(Color color)
    {
        upgradeImage.color = color;

        if(lockedSign != null) lockedSign.GetComponent<Image>().color = color;

        if (selectSign != null) selectSign.GetComponent<Image>().color = color;

        if (frameImage != null) frameImage.color = color;
    }
}
