using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    private const float OPACITY_PURCHASED = .6f;
    private const float OPACITY_NOT_PURCHASED = .2f;

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

    [Header("Floating Panel")]
    public string nameText;
    public string descriptionText;
    public GameObject floatingPanelPrefab;
    [SerializeField] GameObject floatingPanelObj;
    [SerializeField] FloatingPanelShop floatingPanel;

    void Start()
    {
        lockedSign = Instantiate(lockedSignPrefab, transform);
        lockedSign.GetComponent<Image>().color = upgradeImage.color;
        lockedSign.SetActive(false);

        selectSign = Instantiate(selectSignPrefab, transform);
        selectSign.GetComponent<Image>().color = upgradeImage.color;
        selectSign.SetActive(false);

        frame = Instantiate(framePrefab, transform);
        frameImage = frame.GetComponent<Image>();
        frameImage.color = upgradeImage.color;
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
        }
        else
        {
            selectSign.SetActive(false);

            if (floatingPanelObj != null)
            {
                if (floatingPanelObj.activeSelf) floatingPanelObj.SetActive(false);
            }
        }

        lockedSign.SetActive(!upgradeControl.Purchased);
        frameImage.color = new Color(frameImage.color.r, frameImage.color.g, frameImage.color.b, upgradeControl.Purchased ? OPACITY_NOT_PURCHASED : OPACITY_PURCHASED);
    }

    public UpgradeController GetUpgradeControl()
    {
        return upgradeControl;
    }
}
