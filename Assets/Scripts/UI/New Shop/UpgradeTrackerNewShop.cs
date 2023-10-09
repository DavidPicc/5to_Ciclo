using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeTrackerNewShop : MonoBehaviour
{
    public static UpgradeTrackerNewShop instance;

    [SerializeField] SectionButtonControl[] sectionControllers;
    [SerializeField] UpgradeController[] upgradeControllers;

    [System.Serializable]
    public struct Upgrade
    {
        public string feature;
        public string upgrade;
        public int level;
    }

    [SerializeField] Upgrade[] upgrades;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sectionControllers = FindObjectsOfType<SectionButtonControl>();
        upgradeControllers = FindObjectsOfType<UpgradeController>();

        SetUpgradesUI();

        var branches = FindObjectsOfType<BranchNavigator>();

        for (int i = 0; i < branches.Length; i++)
        {
            branches[i].gameObject.SetActive(false);
        }

        GameObject.FindGameObjectWithTag("shopMenu").SetActive(false);
    }

    void SetUpgradesUI()
    {
        for(int i = 0; i < upgrades.Length; i++)
        {
            //Sections
            SectionButtonControl[] sections = Array.FindAll(sectionControllers, (e) => e.feature == upgrades[i].feature && e.upgrade == upgrades[i].upgrade && e.level <= upgrades[i].level);

            for(int j = 0; j < sections.Length; j++)
            {
                sections[j].Purchase();
            }

            //Upgrades
            UpgradeController[] upgradesControls = Array.FindAll(upgradeControllers, (e) => e.feature == upgrades[i].feature && e.upgrade == upgrades[i].upgrade && e.level <= upgrades[i].level);

            for (int j = 0; j < upgradesControls.Length; j++)
            {
                upgradesControls[j].Purchase();
            }
        }
    }

    public void LevelUp(string feature, string upgrade, int level)
    {
        Upgrade upgradeToLevelUp = Array.Find(upgrades, (e) => e.feature == feature && e.upgrade == upgrade);

        if(upgradeToLevelUp.level < level)
        {
            int indexOfUpgrade = Array.IndexOf(upgrades, upgradeToLevelUp);

            upgrades[indexOfUpgrade].level = level;
        }
    }
}
