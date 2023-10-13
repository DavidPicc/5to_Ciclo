using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] UpgradeController upgradeControl;

    [Header("View")]
    public RectTransform upgradeTransform;
    public Image upgradeImage;
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
        initialScale = upgradeTransform.localScale;
        multipliedScale = initialScale * scaleMultiplier;
    }

    void Update()
    {
        UpdateView();
    }

    void UpdateView()
    {
        if (upgradeControl.Selected)
        {
            upgradeTransform.localScale = multipliedScale;

            if (floatingPanelPrefab != null)
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
                floatingPanel.SetPrice(upgradeControl.Purchased ? "" : "Price: " + upgradeControl.gearsCost + " gears " + upgradeControl.coresCost + " cores");
            }
        }
        else
        {
            upgradeTransform.localScale = initialScale;

            if (floatingPanelObj != null)
            {
                if (floatingPanelObj.activeSelf) floatingPanelObj.SetActive(false);
            }
        }

        if (upgradeControl.Purchased)
        {
            upgradeImage.color = new Color(upgradeImage.color.r, upgradeImage.color.g, upgradeImage.color.b, 1f);
        }
        else
        {
            upgradeImage.color = new Color(upgradeImage.color.r, upgradeImage.color.g, upgradeImage.color.b, alphaUnselected);
        }
    }

    public UpgradeController GetUpgradeControl()
    {
        return upgradeControl;
    }
}
