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

    public int gearsCost;
    public int coresCost;

    IEnumerator purchaseRoutine;

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
    }

    IEnumerator StartPurchase()
    {
        if (GameScoreNewShop.instance.gears > gearsCost && GameScoreNewShop.instance.cores > coresCost)
        {
            yield return new WaitForSecondsRealtime(1.5f);

            locked = false;
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
}
