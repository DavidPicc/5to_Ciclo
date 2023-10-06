using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchNavigator : MonoBehaviour
{
    [SerializeField] bool control;
    public bool Control { get { return control; } set { control = value; } }

    [SerializeField] SectionNavigator sectionNavigator;

    [Header("Navigation Debug")]
    [SerializeField] int branchSelected;
    [SerializeField] int upgradeSelected;
    [SerializeField] UpgradeController upgradeSelectedController;

    [System.Serializable]
    public struct Branch 
    {
        public string branchName;
        public UpgradeController[] upgrades;
    }

    [Header("Branches")]
    [SerializeField] public Branch[] branches;

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
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(upgradeSelectedController == centralUpgrade)
            {
                if (!centralUpgrade.Locked)
                {
                    branchSelected = Mathf.FloorToInt(branches.Length / 2);
                    upgradeSelected = 0;
                    UpdateSelectedController();
                }
            } else if(upgradeSelected < branches[branchSelected].upgrades.Length - 1 && !upgradeSelectedController.Locked)
            {
                upgradeSelected++;
                UpdateSelectedController();
            }


        } else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(upgradeSelected == 0)
            {
                ReturnToCentralNode();
            }
            else
            {
                upgradeSelected--;
                UpdateSelectedController();
            }
        } else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (upgradeSelectedController != centralUpgrade && branchSelected < branches.Length - 1)
            {
                branchSelected++;
                upgradeSelected = GetHighestUpgradeUnlokced(upgradeSelected);
                UpdateSelectedController();
            }
        } else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (upgradeSelectedController != centralUpgrade && branchSelected > 0)
            {
                branchSelected--;
                upgradeSelected = GetHighestUpgradeUnlokced(upgradeSelected);
                UpdateSelectedController();
            }
            else
            {
                ReturnToCentralNode();
                control = false;
                upgradeSelectedController.Selected = false;
                sectionNavigator.Control = true;
            }
        }
    }

    void ReturnToCentralNode()
    {
        upgradeSelectedController.Selected = false;
        upgradeSelectedController.PreventiveUpgradeCancel();

        centralUpgrade.Selected = true;

        upgradeSelectedController = centralUpgrade;
    }

    void UpdateSelectedController()
    {
        upgradeSelectedController.Selected = false;
        upgradeSelectedController.PreventiveUpgradeCancel();

        branches[branchSelected].upgrades[upgradeSelected].Selected = true;

        upgradeSelectedController = branches[branchSelected].upgrades[upgradeSelected];
    }

    int GetHighestUpgradeUnlokced(int max)
    {
        for(int i = 0; i <= max && i < branches[branchSelected].upgrades.Length; i++)
        {
            if (branches[branchSelected].upgrades[i].Locked || i == max || i == branches[branchSelected].upgrades.Length - 1)
            {
                return i;
            }
        }

        return 0;
    }

    public void GiveControl()
    {
        upgradeSelectedController.Selected = true;
    }
}
