using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] bool purchased;
    [SerializeField] bool isGun;
    [SerializeField] GameObject purchasedText;

    public bool Equipped
    {
        get { return equipped; }
        set { equipped = value; }
    }

    public bool Purchased
    {
        get { return  purchased; }
        set { purchased = value; }
    }

    public bool IsGun
    {
        get { return isGun; }
    }

    public Animator upgradingAnim;

    [SerializeField] TextMeshProUGUI costGearText, costCoreText, labelText;
    string type;
    public string Type
    {
        get { return type; }
        set { type = value; }
    }
    public int level;
    public int costGear;
    public int costCore;

    public string title;
    public string description;
    public Sprite caption;


    void Start()
    {
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

        if (!purchased)
        {
            costGearText.text = costGear.ToString();
            costCoreText.text = costCore.ToString();
            if (purchasedText.activeSelf) purchasedText.SetActive(false);
        }
        else
        {
            costGearText.text = "";
            costCoreText.text = "";
            if (!purchasedText.activeSelf) purchasedText.SetActive(true);
        }
    }

    public void Purchase()
    {
        purchased = true;
        UpgradeTrackerAct.instance.levels[type] = level;
    }
}
