using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] bool selected;
    public bool Selected { get { return selected; } set { selected = value; } }

    [SerializeField] bool locked;
    public bool Locked { get { return locked; } set { locked = value; } }

    [SerializeField] bool upgrading;

    [SerializeField] UpgradeController nextUpgrade;
    [SerializeField] UpgradeController previousUpgrade;

    public int gearsCost;
    public int coresCost;

    IEnumerator purchaseRoutine;

    [SerializeField] int order;

    [SerializeField] BranchNavigator branchNavigator;

    void Awake()
    {
        locked = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && selected && locked)
        {
            purchaseRoutine = StartPurchase();
            StartCoroutine(purchaseRoutine);
            
            upgrading = true;
        }

        if(Input.GetKeyUp(KeyCode.Z) && upgrading)
        {
            if (purchaseRoutine != null)
            {
                StopCoroutine(purchaseRoutine);   
            }

            upgrading = false;
        }

        if(selected) NavigateUpgrades();
    }

    IEnumerator StartPurchase()
    {
        if (GameScoreNewShop.instance.gears > gearsCost && GameScoreNewShop.instance.cores > coresCost)
        {
            yield return new WaitForSecondsRealtime(1.5f);

            locked = false;
        }
    }

    public void NavigateUpgrades()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(nextUpgrade != null && !locked)
            {
                selected = false;
                nextUpgrade.Selected = true;

                if (upgrading)
                {
                    if (purchaseRoutine != null)
                    {
                        StopCoroutine(purchaseRoutine);
                    }

                    upgrading = false;
                }
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (previousUpgrade != null)
            {
                selected = false;
                previousUpgrade.Selected = true;

                if (upgrading)
                {
                    if (purchaseRoutine != null)
                    {
                        StopCoroutine(purchaseRoutine);
                    }

                    upgrading = false;
                }
            }

            if (previousUpgrade == null && order == 0)
            {
                if (upgrading)
                {
                    if (purchaseRoutine != null)
                    {
                        StopCoroutine(purchaseRoutine);
                    }

                    upgrading = false;
                }

                branchNavigator.ReturnToCentralUpgrade();
            }
        }
    }

    public UpgradeController GetNextUpgrade()
    {
        return nextUpgrade;
    }

    public UpgradeController GetPreviousUpgrade()
    {
        return previousUpgrade;
    }

    public int GetOrder()
    {
        return order;
    }
}
