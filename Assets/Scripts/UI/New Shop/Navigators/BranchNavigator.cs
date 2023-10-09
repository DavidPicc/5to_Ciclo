using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchNavigator : MonoBehaviour
{
    [SerializeField] bool control;
    public bool Control { get { return control; } set { control = value; } }

    [SerializeField] SectionNavigator sectionNavigator;

    [Header("Navigation Debug")]
    [SerializeField] UpgradeController upgradeSelectedController;

    public UpgradeController centralUpgrade;

    private void Start()
    {
        upgradeSelectedController = centralUpgrade;
        centralUpgrade.Selected = true;
    }

    void Update()
    {
        if (!control) return;

        BranchNavigation();
    }

    void BranchNavigation()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetupgradeSelectedController(upgradeSelectedController.GetButtonAtRight());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (upgradeSelectedController.GetButtonAtLeft() == null)
            {
                GiveControlToSection();
            } else if(upgradeSelectedController.GetButtonAtLeft().Locked) GiveControlToSection();
            else SetupgradeSelectedController(upgradeSelectedController.GetButtonAtLeft());
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetupgradeSelectedController(upgradeSelectedController.GetButtonUpwards());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetupgradeSelectedController(upgradeSelectedController.GetButtonDownwards());
        }
    }

    void SetupgradeSelectedController(UpgradeController newUpgradeSelectedController)
    {
        if (newUpgradeSelectedController == null || newUpgradeSelectedController.Locked) return;

        if (upgradeSelectedController != null)
        {
            upgradeSelectedController.Selected = false;
        }

        newUpgradeSelectedController.Selected = true;
        upgradeSelectedController = newUpgradeSelectedController;
    }

    public void GiveControl()
    {
        centralUpgrade.Selected = true;
    }

    void GiveControlToSection()
    {
        control = false;
        upgradeSelectedController.Selected = false;
        upgradeSelectedController = centralUpgrade;
        sectionNavigator.Control = true;
    }
}
