using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    ShopMenu shop;
    public ShopMenu Shop
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
        get { return  type; }
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

        if(level < maxLevel)
        {
            costGearText.text = costGear.ToString();
            costCoreText.text = costCore.ToString();
            if(maxLevelText.activeSelf) maxLevelText.SetActive(false);
        } else
        {
            costGearText.text = "";
            costCoreText.text = "";
            if (!maxLevelText.activeSelf) maxLevelText.SetActive(true);
        }
    }

    public void Upgrade()
    {
        if(level < maxLevel)
        {
            level++;
            costCore += upgradeCoreScaling;
            costGear += upgradeGearScaling;

            UpgradeTracker.instance.levels[type] = level;
        }
    }

    public void SetStartLevel()
    {
        if (UpgradeTracker.instance.levels.ContainsKey(type))
        {
            level = UpgradeTracker.instance.levels[type];
            if(level > maxLevel)
            {
                level = maxLevel;
                UpgradeTracker.instance.levels[type] = level;
            }

        } else UpgradeTracker.instance.levels.Add(type, level);

        equipped = UpgradeTracker.instance.equippedGun == type || UpgradeTracker.instance.equippedSkill == type;

        if(level > 0) costCore += upgradeCoreScaling * (level - 1);
        if(level > 0) costGear += upgradeGearScaling * (level - 1);
    }
}
