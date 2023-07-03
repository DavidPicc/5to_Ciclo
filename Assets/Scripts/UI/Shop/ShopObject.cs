using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopObject : MonoBehaviour
{
    [SerializeField] protected int level;
    [SerializeField] protected string type;
    [SerializeField] protected bool equipped;

    protected void OnEnable()
    {
        GameManager.onShopApply += Shopping;
    }

    protected void OnDisable()
    {
        GameManager.onShopApply -= Shopping;
    }

    protected virtual void Start()
    {
        Shopping();
    }

    protected virtual void Shopping()
    {
        if(UpgradeTrackerAct.instance == null)
        {
            return;
        }
        if (UpgradeTrackerAct.instance.levels.ContainsKey(type))
        {
            level = UpgradeTrackerAct.instance.levels[type];
        }
        else UpgradeTrackerAct.instance.levels.Add(type, level);

        equipped = UpgradeTrackerAct.instance.equippedSkill == type || UpgradeTrackerAct.instance.equippedGun == type;
    }
}
