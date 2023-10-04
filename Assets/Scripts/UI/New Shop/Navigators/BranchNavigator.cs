using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchNavigator : MonoBehaviour
{
    [SerializeField] bool control;
    public bool Control { get { return control; } set { control = value; } }

    public int branchNumber;
    [SerializeField] int branchSelected;

    public UpgradeController[] startUpgrades;   

    void Update()
    {
        if (!control) return;

        BranchNavigation();
    }

    void BranchNavigation()
    {
        if(branchSelected == 0)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                int order = GetActiveUpgrade().GetOrder();
                DeactivateBranch();
                branchSelected = (int) Mathf.Floor(branchNumber/2);
                ReactiveBranch(order);
            }
        } else if(branchSelected < branchNumber)
        {
            if(Input.GetKeyDown(KeyCode.RightArrow) && branchSelected < branchNumber){
                int order = GetActiveUpgrade().GetOrder();
                DeactivateBranch();
                branchSelected++;
                ReactiveBranch(order);
            } else if(Input.GetKeyDown(KeyCode.LeftArrow) && branchSelected > 1)
            {
                int order = GetActiveUpgrade().GetOrder();
                DeactivateBranch();
                branchSelected--;
                ReactiveBranch(order);
            }
        }
    }

    UpgradeController GetActiveUpgrade()
    {
        var _temp = startUpgrades[branchSelected];

        do
        {
            if (_temp.Selected)
            {
                return _temp;
            }
            else _temp = _temp.GetNextUpgrade();
        } while (_temp != null);

        return default(UpgradeController);
    }

    void DeactivateBranch()
    {
        var _temp = startUpgrades[branchSelected];

        while (_temp != null)
        {
            _temp.Selected = false;

            _temp = _temp.GetNextUpgrade();
        }
    }

    void ReactiveBranch(int order)
    {
        var _temp = startUpgrades[branchSelected];

        for(int i = 0; i < order; i++)
        {
            if(_temp != null)
            {
                if (_temp.Locked) break;
            }

            _temp = _temp.GetNextUpgrade();
        }

        _temp.Selected = true;
    }

    public void ReturnToCentralUpgrade()
    {
        if (!control || branchSelected == 0) return;

        int order = GetActiveUpgrade().GetOrder();
        DeactivateBranch();
        branchSelected = 0;
        ReactiveBranch(order);
    }
}
