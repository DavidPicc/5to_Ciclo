using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public KeyCode key;
    
    [SerializeField] TextMeshProUGUI gearsScoreShop, coresScoreShop, titleItemShop, equippedGun, equippedSkill;
    [SerializeField] ShopItem[] gunUpgrades;
    [SerializeField] ShopItem[] skillUpgrades;
    int upgradeCount => selectedGuns ? gunUpgrades.Length : skillUpgrades.Length;

    bool selectedGuns;
    int upgradeSelected;

    bool upgrading;
    float upgradingTimer;
    [SerializeField] float upgradeTime;

    void Start()
    {
        SetEquipped();

        foreach(ShopItem gun in gunUpgrades)
        {
            gun.Shop = this;
        }

        foreach(ShopItem skill in skillUpgrades)
        {
            skill.Shop = this;
        }
    }

    void Update()
    {
        ShopItem selectedItem = GetSelectedItem();

        //Labels
        titleItemShop.text = selectedItem.title;
        gearsScoreShop.text = GameScore.instance.gearScore.ToString();
        coresScoreShop.text = GameScore.instance.coreScore.ToString();

        //Navigation
        if (!Input.GetKey(key))
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                int horizontal = (int)Input.GetAxisRaw("Horizontal");
                upgradeSelected = (upgradeSelected + horizontal + upgradeCount) % upgradeCount;
            }

            if (Input.GetButtonDown("Vertical"))
            {
                selectedGuns = !selectedGuns;
                upgradeSelected = Mathf.Clamp(upgradeSelected, 0, upgradeCount - 1);
            }
        }

        //Equipping
        if(Input.GetKeyUp(key) && !upgrading && selectedItem.level > 0)
        {
            EquipSelectedUpgrade();
        }

        //Upgrading
        if(Input.GetKeyDown(key))
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

        //Equipped 
        ShopItem equippedGunItem = Array.Find(gunUpgrades, e => e.Equipped == true);
        ShopItem equippedSkillItem = Array.Find(skillUpgrades, e => e.Equipped == true);

        if (equippedGunItem != null) equippedGun.text = equippedGunItem.title;
        if (equippedSkillItem != null) equippedSkill.text = equippedSkillItem.title;
    }

    public ShopItem GetSelectedItem()
    {
        if(selectedGuns) return gunUpgrades[upgradeSelected];
        else return skillUpgrades[upgradeSelected];
    }

    IEnumerator Upgrade()
    {
        while(upgradingTimer < upgradeTime)
        {
            ShopItem selectedItemTime = GetSelectedItem();

            if (upgradingTimer > .2f) upgrading = true;

            if (upgrading && GameScore.instance.gearScore >= selectedItemTime.costGear && GameScore.instance.coreScore >= selectedItemTime.costCore && selectedItemTime.level < selectedItemTime.maxLevel)
            {
                selectedItemTime.upgradingAnim.SetFloat("fill", upgradingTimer / upgradeTime);
            }

            yield return new WaitForSecondsRealtime(.1f);
            upgradingTimer += .1f;
        }

        ShopItem selectedItem = GetSelectedItem();

        if (GameScore.instance.gearScore >= selectedItem.costGear && GameScore.instance.coreScore >= selectedItem.costCore && selectedItem.level < selectedItem.maxLevel)
        {
            upgradingTimer = 0f;
            selectedItem.upgradingAnim.SetFloat("fill", 0f);

            GameScore.instance.RemoveGears(selectedItem.costGear);
            GameScore.instance.RemoveCores(selectedItem.costCore);
            selectedItem.Upgrade();

            //Automatic Equipping
            EquipSelectedUpgrade();
        }

        if(Input.GetKey(key)) StartCoroutine(Upgrade());
    }

    void EquipSelectedUpgrade()
    {
        if (selectedGuns)
        {
            for (int i = 0; i < gunUpgrades.Length; i++)
            {
                if (i != upgradeSelected) gunUpgrades[i].Equipped = false;
                else
                {
                    gunUpgrades[i].Equipped = true;
                    UpgradeTracker.instance.equippedGun = gunUpgrades[i].Type;
                }
            }
        }
        else
        {
            for (int i = 0; i < skillUpgrades.Length; i++)
            {
                if (i != upgradeSelected) skillUpgrades[i].Equipped = false;
                else
                {
                    skillUpgrades[i].Equipped = true;
                    UpgradeTracker.instance.equippedSkill = skillUpgrades[i].Type;
                }
            }
        }
    }

    void SetEquipped()
    {
        //Gun Equipped
        if(UpgradeTracker.instance.equippedGun != "")
        {     
            foreach(ShopItem shopItem in gunUpgrades)
            {
                if(shopItem.Type == UpgradeTracker.instance.equippedGun) shopItem.Equipped = true;
                else shopItem.Equipped = false;
            }
        } else
        {
            ShopItem shopItem = Array.Find(gunUpgrades, e => e.Equipped == true);

            if(shopItem != null) UpgradeTracker.instance.equippedGun = shopItem.Type;
        }

        //Skill Equipped
        if (UpgradeTracker.instance.equippedSkill != "")
        {
            foreach (ShopItem shopItem in skillUpgrades)
            {
                if (shopItem.Type == UpgradeTracker.instance.equippedSkill) shopItem.Equipped = true;
                else shopItem.Equipped = false;
            }
        } else
        {
            ShopItem shopItem = Array.Find(skillUpgrades, e => e.Equipped == true);

            if(shopItem != null) UpgradeTracker.instance.equippedSkill = shopItem.Type;
        }
    }
}
