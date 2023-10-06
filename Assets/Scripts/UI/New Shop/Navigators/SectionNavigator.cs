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

    [Header("Branches")]
    public BranchesNavigator branchesNavigator;

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
            if (hoverButton.GetButtonAtRight() == null)
            {
                GiveControlToBranches();
            } else SetHoverButton(hoverButton.GetButtonAtRight());
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

        branchesNavigator.ActivateBranch(hoverButton.branch);
    }

    void GiveControlToBranches()
    {
        var branches = branchesNavigator.GetBranches();

        for(int i = 0; i < branches.Length; i++)
        {
            if (branches[i].activeSelf)
            {
                BranchNavigator branchNav = branches[i].GetComponent<BranchNavigator>();
                if(branchNav != null)
                {
                    control = false;
                    branchNav.Control = true;
                    branchNav.GiveControl();
                }
            }
        }
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
