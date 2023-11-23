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
    public SectionButtonControl sectionButtonControl;

    private void Start()
    {
        upgradeSelectedController = centralUpgrade;
        centralUpgrade.Selected = true;
    }

    void Update()
    {
        if (!control) return;

        BranchNavigation();
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape)) GiveControlToSection();
    }

    void BranchNavigation()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetUpgradeSelectedController(upgradeSelectedController.GetButtonAtRight());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //if (upgradeSelectedController.GetButtonAtLeft() == null)
            //{
            //    GiveControlToSection();
            //} else if(upgradeSelectedController.GetButtonAtLeft().Locked) GiveControlToSection();
            //else SetUpgradeSelectedController(upgradeSelectedController.GetButtonAtLeft());

            if (upgradeSelectedController.GetButtonAtLeft() != null && !upgradeSelectedController.GetButtonAtLeft().Locked)
            {
                SetUpgradeSelectedController(upgradeSelectedController.GetButtonAtLeft());
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetUpgradeSelectedController(upgradeSelectedController.GetButtonUpwards());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetUpgradeSelectedController(upgradeSelectedController.GetButtonDownwards());
        }
    }

    void SetUpgradeSelectedController(UpgradeController newUpgradeSelectedController)
    {
        if (newUpgradeSelectedController == null || newUpgradeSelectedController.Locked) return;

        if (upgradeSelectedController != null)
        {
            upgradeSelectedController.Selected = false;
        }

        FindObjectOfType<ShopSFX>().NavSFX();
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
        gameObject.SetActive(false);
        FindObjectOfType<ShopSFX>().BackSFX();
    }
}
