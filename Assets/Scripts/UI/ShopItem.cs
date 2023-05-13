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
    bool equipped;
    public bool Equipped
    {
        set { equipped = value; }
    }

    public Animator upgradingAnim;

    [SerializeField] TextMeshProUGUI costGearText, costCoreText, labelText;
    [SerializeField] string type;
    public int level;
    public int costGear;
    public int costCore;
    public int upgradeGearScaling;
    public int upgradeCoreScaling;

    public string title;

    void Start()
    {
        level = 1;

        upgradingAnim.SetFloat("fill", 0f);
    }

    void Update()
    {
        if (shop.GetSelectedItem() == this) selected = true;
        else selected = false;

        if (selected != selectedFrame.activeSelf) selectedFrame.SetActive(selected);
    
        if (equipped != equippedText.activeSelf) equippedText.SetActive(equipped);

        labelText.text = title + " " + level;

        costGearText.text = costGear.ToString();
        costCoreText.text = costCore.ToString();
    }

    public void Upgrade()
    {
        level++;
        costCore += upgradeCoreScaling;
        costGear += upgradeGearScaling;

        UpgradeTracker.instance.levels[type] = level;
    }

}
