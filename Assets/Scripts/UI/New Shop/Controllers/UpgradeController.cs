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
    public GameObject lineRendererPrefab;
    [SerializeField] UILineRenderer[] linesToNextUpgrades;
    public Color lockedColorLine = Color.black;
    public Color unlockedColorLine = Color.green;

    [Header("Tracker")]
    public string feature;
    public string upgrade;
    public int level;

    private void Start()
    {
        linesToNextUpgrades = new UILineRenderer[nextUpgrades.Length];

        for(int i = 0; i < linesToNextUpgrades.Length; i++) 
        {
            var _obj = Instantiate(lineRendererPrefab, transform.parent);
            linesToNextUpgrades[i] = _obj.GetComponent<UILineRenderer>();

            linesToNextUpgrades[i].DrawLine(GetComponent<RectTransform>(), nextUpgrades[i].GetComponent<RectTransform>());
            linesToNextUpgrades[i].SetColor(purchased ? unlockedColorLine : lockedColorLine);
        }
    }

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
        if (GameScoreNewShop.instance.gears >= gearsCost && GameScoreNewShop.instance.cores >= coresCost)
        {
            yield return new WaitForSecondsRealtime(PURCHASE_TIME);

            upgrading = false;
            GameScoreNewShop.instance.Spend(gearsCost, coresCost);
            Purchase();
            if (UpgradeTrackerNewShop.instance != null) UpgradeTrackerNewShop.instance.LevelUp(feature, upgrade, level);
        }
    }

    public void Purchase()
    {
        purchased = true;
        UnlockNextUpgrades();

        for (int i = 0; i < linesToNextUpgrades.Length; i++)
        {
            linesToNextUpgrades[i].SetColor(unlockedColorLine);
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

    public UILineRenderer[] GetLinesToUpgrades()
    {
        return linesToNextUpgrades;
    }
}
