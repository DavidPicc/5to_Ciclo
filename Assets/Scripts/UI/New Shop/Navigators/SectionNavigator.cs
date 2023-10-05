using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionNavigator : MonoBehaviour
{
    [SerializeField] bool control;
    public bool Control { get { return control; } set { control = value; } }

    SectionButtonControl hoverButton;
    SectionButtonControl selectedButton;

    [Header("Start Button")]
    public SectionButtonControl startButton;

    void Start()
    {
        SetHoverButton(startButton);
        SelectButton();
    }

    void Update()
    {
        if (!control) return;

        ButtonNavigation();
        if(Input.GetKeyDown(KeyCode.Z)) SelectButton();
    }

    void ButtonNavigation()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetHoverButton(hoverButton.GetButtonAtRight());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetHoverButton(hoverButton.GetButtonAtLeft());
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetHoverButton(hoverButton.GetButtonUpwards());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetHoverButton(hoverButton.GetButtonDownwards());
        }
    }

    void SetHoverButton(SectionButtonControl newHoverButton)
    {
        if (newHoverButton == null) return;

        if(hoverButton != null)
        {
            hoverButton.Hover = false;
        }

        newHoverButton.Hover = true;
        hoverButton = newHoverButton;
    }

    void SelectButton()
    {
        if(selectedButton == hoverButton) return;

        if(selectedButton != null)
        {
            selectedButton.Selected = false;
        }

        hoverButton.Selected = true;
        selectedButton = hoverButton;
    }

    public SectionButtonControl GetHoverButton() 
    { 
        return hoverButton;
    }

    public SectionButtonControl GetSelectedButton()
    {
        return selectedButton;
    }
}
