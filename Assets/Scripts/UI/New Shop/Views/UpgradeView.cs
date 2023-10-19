using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] UpgradeController upgradeControl;

    [Header("View")]
    public Image upgradeImage;
    public GameObject lockedSignPrefab;
    GameObject lockedSign;

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
    }

    void Update()
    {
        UpdateView();
    }

    void UpdateView()
    {
        if (upgradeControl.Selected)
        {

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
            if (floatingPanelObj != null)
            {
                if (floatingPanelObj.activeSelf) floatingPanelObj.SetActive(false);
            }
        }

        lockedSign.SetActive(!upgradeControl.Purchased);
    }

    public UpgradeController GetUpgradeControl()
    {
        return upgradeControl;
    }
}
