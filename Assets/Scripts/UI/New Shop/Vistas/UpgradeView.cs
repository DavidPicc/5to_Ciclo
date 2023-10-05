using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        }
        else
        {
            upgradeTransform.localScale = initialScale;
        }

        if (!upgradeControl.Locked)
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
