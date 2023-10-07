using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    private const float PURCHASE_TIME = 1f;

    [SerializeField] bool selected;
    public bool Selected { get { return selected; } set { selected = value; } }

    [SerializeField] bool locked;
    public bool Locked { get { return locked; } set { locked = value; } }

    [SerializeField] bool purchased;
    public bool Purchased { get { return purchased; } set { purchased = value; } }

    [SerializeField] bool upgrading;

    public int gearsCost;
    public int coresCost;

    IEnumerator purchaseRoutine;

    [SerializeField] BranchNavigator branchNavigator;

    [Header("Navigation")]
    [SerializeField] UpgradeController buttonAtRight;
    [SerializeField] UpgradeController buttonAtLeft;
    [SerializeField] UpgradeController buttonUpwards;
    [SerializeField] UpgradeController buttonDownwards;

    [Header("Next Upgrades")]
    [SerializeField] UpgradeController[] nextUpgrades;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && selected && !purchased)
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
    }

    IEnumerator StartPurchase()
    {
        if (GameScoreNewShop.instance.gears > gearsCost && GameScoreNewShop.instance.cores > coresCost)
        {
            yield return new WaitForSecondsRealtime(PURCHASE_TIME);

            purchased = true;
            upgrading = false;
            UnlockNextUpgrades();
            GameScoreNewShop.instance.Spend(gearsCost, coresCost);
        }
    }

    public void PreventiveUpgradeCancel()
    {
        if (upgrading)
        {
            if (purchaseRoutine != null)
            {
                StopCoroutine(purchaseRoutine);
            }

            upgrading = false;
        }
    }

    void UnlockNextUpgrades()
    {
        for(int i = 0; i < nextUpgrades.Length; i++)
        {
            if (nextUpgrades[i] != null)
            {
                nextUpgrades[i].Locked = false;
            }
        }
    }

    public UpgradeController GetButtonAtRight()
    {
        return buttonAtRight;
    }

    public UpgradeController GetButtonAtLeft()
    {
        return buttonAtLeft;
    }

    public UpgradeController GetButtonUpwards()
    {
        return buttonUpwards;
    }

    public UpgradeController GetButtonDownwards()
    {
        return buttonDownwards;
    }
}
