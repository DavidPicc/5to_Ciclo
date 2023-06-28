using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuAct : MonoBehaviour
{
    public KeyCode key;

    [SerializeField] TextMeshProUGUI gearsScoreShop, coresScoreShop, titleItemShop, descriptionText, costGearsText, costCoresText;
    [SerializeField] Image image;
    List<ShopItemAct> flattenUpgrades;

    [System.Serializable]
    public struct UpgradeBranch
    {
        public ShopItemAct[] upgrades;
        public string type;
    }

    [SerializeField] public UpgradeBranch[] branches;


    int upgradeSelected;
    int upgradeLevel;

    bool upgrading;
    float upgradingTimer;
    [SerializeField] float upgradeTime;

    void Start()
    {
        flattenUpgrades = new List<ShopItemAct>();

        foreach (UpgradeBranch branch in branches)
        {
            foreach (ShopItemAct upgrade in branch.upgrades)
            {
                upgrade.Shop = this;
                upgrade.Type = branch.type;
                //Flatten List of Upgrades
                flattenUpgrades.Add(upgrade);
            }
        }

        SetUpgradeKeysStart();
        SetEquippedStart();
    }

    void Update()
    {
        ShopItemAct selectedItem = GetSelectedItem();

        //Labels
        titleItemShop.text = selectedItem.title;
        gearsScoreShop.text = GameScore.instance.gearScore.ToString();
        coresScoreShop.text = GameScore.instance.coreScore.ToString();
        descriptionText.text = selectedItem.description;
        costGearsText.text = selectedItem.costGear.ToString();
        costCoresText.text = selectedItem.costCore.ToString();
        image.sprite = selectedItem.caption;

        //Navigation
        if (!Input.GetKey(key))
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                int horizontal = (int)Input.GetAxisRaw("Horizontal");
                upgradeSelected = (upgradeSelected + horizontal + branches.Length) % branches.Length;
                if (horizontal != 0) {
                    upgradeLevel = Mathf.Max(UpgradeTrackerAct.instance.levels[branches[upgradeSelected].upgrades[0].Type], 1);
                    
                }
            }

            if (Input.GetButtonDown("Vertical"))
            {
                if (upgradeLevel == UpgradeTrackerAct.instance.levels[branches[upgradeSelected].type]) upgradeLevel++;
                else upgradeLevel--;

                upgradeLevel = Mathf.Clamp(upgradeLevel, 1, branches[upgradeSelected].upgrades.Length);
            }
        }

        //Equipping
        if (Input.GetKeyUp(key) && !upgrading && selectedItem.level == UpgradeTrackerAct.instance.levels[selectedItem.Type])
        {
            SetEquippedUpgrades();
        }

        //Upgrading
        if (Input.GetKeyDown(key))
        {
            StartCoroutine(Upgrade());
        }

        if (Input.GetKeyUp(key))
        {
            upgrading = false;
            upgradingTimer = 0f;
            selectedItem.upgradingAnim.SetFloat("fill", 0f);

            StopAllCoroutines();
        }
    }

    public ShopItemAct GetSelectedItem()
    {
        return branches[upgradeSelected].upgrades[upgradeLevel - 1];
    }

    IEnumerator Upgrade()
    {
        while (upgradingTimer < upgradeTime)
        {
            ShopItemAct selectedItemTime = GetSelectedItem();

            if (upgradingTimer > .2f) upgrading = true;

            if (upgrading && GameScore.instance.gearScore >= selectedItemTime.costGear && GameScore.instance.coreScore >= selectedItemTime.costCore && !selectedItemTime.Purchased)
            {
                selectedItemTime.upgradingAnim.SetFloat("fill", upgradingTimer / upgradeTime);
            }

            yield return new WaitForSecondsRealtime(.1f);
            upgradingTimer += .1f;
        }

        ShopItemAct selectedItem = GetSelectedItem();

        if (GameScore.instance.gearScore >= selectedItem.costGear && GameScore.instance.coreScore >= selectedItem.costCore && !selectedItem.Purchased)
        {
            upgradingTimer = 0f;
            selectedItem.upgradingAnim.SetFloat("fill", 0f);

            GameScore.instance.RemoveGears(selectedItem.costGear);
            GameScore.instance.RemoveCores(selectedItem.costCore);
            selectedItem.Purchase();

            //Automatic Equipping
            SetEquippedUpgrades();
        }

        if (Input.GetKey(key)) StartCoroutine(Upgrade());
    }

    void SetEquippedUpgrades()
    {
        ShopItemAct selectedItem = GetSelectedItem();

        foreach(ShopItemAct upgrade in flattenUpgrades)
        {
            if (upgrade == selectedItem)
            {
                upgrade.Equipped = true;
                if(upgrade.IsGun) UpgradeTrackerAct.instance.equippedGun = upgrade.Type;
                else UpgradeTrackerAct.instance.equippedSkill = upgrade.Type;
            } else if(upgrade.IsGun == selectedItem.IsGun)
            {
                upgrade.Equipped = false;
            }
        }
    }

    void SetUpgradeKeysStart()
    {
        foreach (UpgradeBranch upgradeBranch in branches)
        {
            if (UpgradeTrackerAct.instance.levels.ContainsKey(upgradeBranch.type))
            {
                foreach (ShopItemAct upgrade in upgradeBranch.upgrades)
                {
                    if (upgrade.level <= UpgradeTrackerAct.instance.levels[upgrade.Type]) upgrade.Purchased = true;
                }
            }
            else UpgradeTrackerAct.instance.levels.Add(upgradeBranch.type, 0);

            //Set start upgrade level based on first upgrade branch level
            if (branches[0].type == branches[0].type)
            {
                upgradeLevel = Mathf.Max(UpgradeTrackerAct.instance.levels[upgradeBranch.type], 1);
            }
        }
    }

    void SetEquippedStart()
    {
        foreach (ShopItemAct upgrade in flattenUpgrades)
        {
            //Evaluation depending of nature of upgrade (Gun or Skill)
            if (upgrade.IsGun)
            {
                if(UpgradeTrackerAct.instance.equippedGun != "")
                {
                    //Equipped Gun is already setted by tracker
                    upgrade.Equipped = false;
                    if(upgrade.Type == UpgradeTrackerAct.instance.equippedGun && UpgradeTrackerAct.instance.levels.ContainsKey(upgrade.Type))
                    {
                        if (upgrade.level == UpgradeTrackerAct.instance.levels[upgrade.Type]) upgrade.Equipped = true;
                    }
                } else if(upgrade.Equipped) UpgradeTrackerAct.instance.equippedGun = upgrade.Type;
            } else
            {
                if (UpgradeTrackerAct.instance.equippedGun != "")
                {
                    //Equipped Skill is already setted by tracker
                    upgrade.Equipped = false;
                    if (upgrade.Type == UpgradeTrackerAct.instance.equippedSkill && UpgradeTrackerAct.instance.levels.ContainsKey(upgrade.Type))
                    {
                        if (upgrade.level == UpgradeTrackerAct.instance.levels[upgrade.Type]) upgrade.Equipped = true;
                    }
                }
                else if (upgrade.Equipped) UpgradeTrackerAct.instance.equippedSkill = upgrade.Type;
            }
        }
    }
}
