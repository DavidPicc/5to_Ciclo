using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionButtonView : MonoBehaviour
{
    [SerializeField] SectionButtonControl buttonControl;

    [Header("View")]
    public RectTransform buttonTransform;
    public Image buttonImage;
    Vector2 initialScale;
    public float scaleMultiplier;
    Vector2 multipliedScale;
    public float alphaUnselected;

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
        if(buttonControl.Hover)
        {
            buttonTransform.localScale = multipliedScale;
        }
        else
        {
            buttonTransform.localScale = initialScale;
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
