using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionNavigator : MonoBehaviour
{
    [SerializeField] bool control;
    public bool Control { get { return control; } set { control = value; } }

    public SectionButtonControl hoverButton;
    public SectionButtonControl selectedButton;

    [Header("Equipment")]
    public string selectedWeapon;
    public string selectedShield;

    [Header("Start Button")]
    public SectionButtonControl startButton;

    [Header("Branches")]
    public BranchesNavigator branchesNavigator;

    void Start()
    {
        SetHoverButton(startButton);
    }

    void Update()
    {
        if (!control) return;

        ButtonNavigation();
        if(Input.GetKey(KeyCode.Z)) SelectButton();
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.ResumeGame();
        }
    }

    void ButtonNavigation()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (hoverButton.GetButtonAtRight() == null)
            {
                GiveControlToBranches();
            } else if (hoverButton.GetButtonAtRight() != null) SetHoverButton(hoverButton.GetButtonAtRight());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (hoverButton.GetButtonAtLeft() != null) SetHoverButton(hoverButton.GetButtonAtLeft());
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(hoverButton.GetButtonUpwards() != null) SetHoverButton(hoverButton.GetButtonUpwards());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (hoverButton.GetButtonDownwards() != null) SetHoverButton(hoverButton.GetButtonDownwards());
        }
    }

    public void SetHoverButton(SectionButtonControl newHoverButton)
    {
        FindObjectOfType<ShopSFX>().NavSFX();

        if (FindObjectOfType<ShopVideos>() != null)
        {
            FindObjectOfType<ShopVideos>().StopVideo();
            if(newHoverButton.GetComponent<SectionButtonView>().videoClip != null) FindObjectOfType<ShopVideos>().PlayVideo(newHoverButton.GetComponent<SectionButtonView>().videoClip);
        }
        if (newHoverButton == null) return;

        if(hoverButton != null)
        {
            hoverButton.Hover = false;
            hoverButton.PreventiveUpgradeCancel();
        }

        newHoverButton.Hover = true;
        hoverButton = newHoverButton;
    }

    public void SelectButton()
    {
        if(selectedButton == hoverButton || !hoverButton.Purchased) return;

        if(selectedButton != null)
        {
            selectedButton.Selected = false;
        }

        hoverButton.Selected = true;
        selectedButton = hoverButton;

        if (selectedButton.isWeapon)
        {
            FindObjectOfType<ShopSFX>().EquipSFX();
            selectedWeapon = selectedButton.feature;
            if(UpgradeTrackerNewShop.instance != null)  UpgradeTrackerNewShop.instance.selectedWeapon = selectedButton.feature;
        }
        else if(selectedButton.isShield)
        {
            FindObjectOfType<ShopSFX>().EquipSFX();
            selectedShield = selectedButton.feature;
            if (UpgradeTrackerNewShop.instance != null) UpgradeTrackerNewShop.instance.selectedShield = selectedButton.feature;
        }

        branchesNavigator.ActivateBranch(selectedButton.branch);
        GiveControlToBranches();

        if (FindObjectOfType<ShopVideos>() != null)
        {
            FindObjectOfType<ShopVideos>().StopVideo();
        }
        StartCoroutine(UpgradeTrackerNewShop.instance.SetEquipment());
    }

    private void GiveControlToBranches()
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

        selectedButton = null;
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
