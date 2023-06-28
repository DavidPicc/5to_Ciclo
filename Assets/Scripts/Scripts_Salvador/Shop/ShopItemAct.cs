using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItemAct : MonoBehaviour
{
    ShopMenuAct shop;
    public ShopMenuAct Shop
    {
        set { shop = value; }
    }

    [SerializeField] GameObject selectedFrame;
    bool selected;

    [SerializeField] GameObject equippedText;
    [SerializeField] bool equipped;
    [SerializeField] GameObject maxLevelText;

    public bool Equipped
    {
        get { return equipped; }
        set { equipped = value; }
    }

    public Animator upgradingAnim;

    [SerializeField] TextMeshProUGUI costGearText, costCoreText, labelText;
    [SerializeField] string type;
    public string Type
    {
        get { return type; }
    }
    public int level;
    public int maxLevel;
    public int costGear;
    public int costCore;
    public int upgradeGearScaling;
    public int upgradeCoreScaling;

    public string title;

    void Start()
    {
        SetStartLevel();
        upgradingAnim.SetFloat("fill", 0f);
    }

    void Update()
    {
        if (shop.GetSelectedItem() == this) selected = true;
        else selected = false;

        if (selected != selectedFrame.activeSelf) selectedFrame.SetActive(selected);

        if (equipped != equippedText.activeSelf) equippedText.SetActive(equipped);

        if (level > 0) labelText.text = title + " " + level;
        else labelText.text = "Comprar " + title;

        if (level < maxLevel)
        {
            costGearText.text = costGear.ToString();
            costCoreText.text = costCore.ToString();
            if (maxLevelText.activeSelf) maxLevelText.SetActive(false);
        }
        else
        {
            costGearText.text = "";
            costCoreText.text = "";
            if (!maxLevelText.activeSelf) maxLevelText.SetActive(true);
        }
    }

    public void Upgrade()
    {
        if (level < maxLevel)
        {
            level++;
            costCore += upgradeCoreScaling;
            costGear += upgradeGearScaling;

            UpgradeTrackerAct.instance.levels[type] = level;
        }
    }

    public void SetStartLevel()
    {
        if (UpgradeTrackerAct.instance.levels.ContainsKey(type))
        {
            level = UpgradeTrackerAct.instance.levels[type];
            if (level > maxLevel)
            {
                level = maxLevel;
                UpgradeTrackerAct.instance.levels[type] = level;
            }

        }
        else UpgradeTrackerAct.instance.levels.Add(type, level);

        equipped = UpgradeTrackerAct.instance.equippedGun == type || UpgradeTrackerAct.instance.equippedSkill == type;

        if (level > 0) costCore += upgradeCoreScaling * (level - 1);
        if (level > 0) costGear += upgradeGearScaling * (level - 1);
    }
}
