using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class SectionButtonControl : MonoBehaviour
{
    private const float PURCHASE_TIME = 1f;

    [SerializeField] bool selected;
    public bool Selected { get { return selected; } set { selected = value; } }

    [SerializeField] bool hover;
    public bool Hover { get { return hover; } set { hover = value; } }

    [SerializeField] bool purchased;
    public bool Purchased { get { return purchased; } set { purchased = value; } }

    [SerializeField] bool upgrading;

    IEnumerator purchaseRoutine;

    [Header("Equipment")]
    public bool isWeapon;
    public bool isShield;

    [Header("Costs")]
    public int gearsCost;
    public int coresCost;

    [Header("Navigation")]
    [SerializeField] SectionButtonControl buttonAtRight;
    [SerializeField] SectionButtonControl buttonAtLeft;
    [SerializeField] SectionButtonControl buttonUpwards;
    [SerializeField] SectionButtonControl buttonDownwards;
    public int branch;

    [Header("Tracker")]
    public string feature;
    public string upgrade;
    public int level;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && hover && !purchased)
        {
            purchaseRoutine = StartPurchase();
            StartCoroutine(purchaseRoutine);

            upgrading = true;
        }

        if (Input.GetKeyUp(KeyCode.Z) && upgrading)
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
        if (GameScoreNewShop.instance.gears >= gearsCost && GameScoreNewShop.instance.cores >= coresCost)
        {
            yield return new WaitForSecondsRealtime(PURCHASE_TIME);

            upgrading = false;
            Purchase();
            GameScoreNewShop.instance.Spend(gearsCost, coresCost);
            if (UpgradeTrackerNewShop.instance != null) UpgradeTrackerNewShop.instance.LevelUp(feature, upgrade, level);
        }
    }

    public void Purchase()
    {
        purchased = true;
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

    public SectionButtonControl GetButtonAtRight()
    {
        return buttonAtRight;
    }

    public SectionButtonControl GetButtonAtLeft()
    {
        return buttonAtLeft;
    }

    public SectionButtonControl GetButtonUpwards()
    {
        return buttonUpwards;
    }

    public SectionButtonControl GetButtonDownwards()
    {
        return buttonDownwards;
    }
}
