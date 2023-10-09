using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeTrackerNewShop : MonoBehaviour
{
    private const float TIME_PER_LEVEL_SHIELD = 1.2f;

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
    
    [Header("Equipment")]
    public string selectedWeapon = "LANZACLAVOS";
    public string selectedShield = "ESCUDOSIERRAS";
    public TextMeshProUGUI selectedWeaponUI;
    public TextMeshProUGUI selectedShieldUI;

    [Header("Upgrade Components")]
    public PlayerMovement_TestSalvador2 playerMovement;
    public Player_Health playerHealth;
    public Player_Shoot playerLanzaclavos;
    public Player_Shoot2 playerLanzagranadas;
    public Player_Shoot3 playerLanzallamas;
    public Player_Ability_Fixed playerEscudoReflector;
    public Player_Ability2 playerEscudoSierras;

    [Header("Lanzaclavos Upgrades")]
    public int level1Cannon;
    public int level1Cannons;
    float baseDamage;
    float baseFireRate;
    int baseCannon;
    int baseCannons;

    [Header("Lanzallamas Upgrades")]
    public float level1Alcance;
    public float level1Radio;
    float baseDamageLanzallamas;
    float baseFireRateLanzallamas;
    float baseDistanceLanzallamas;
    float baseMaxAngleLanzallamas;

    float baseDamageLanzagranadas;
    float baseFireRateLanzagranadas;

    [Header("Escudo de Sierras Upgrades")]
    public int sierrasPorLevel;
    float baseDamageSierras;
    int baseSierrasNum;
    float baseEnergiaSierras;

    [Header("Escudo Reflector Upgrades")]
    public int objetivosPorLevel;
    int baseAbsorcion;
    int baseObjetivos;
    float baseEnergiaReflector;

    [Header("Vindex Health Upgrades")]
    public float invulnerabilityPerLevel;
    public float healthPerLevel;
    float baseHealth;
    float baseInvulnerability;

    [Header("Vindex Movement Upgrades")]
    public float levelAcceleration;
    public float levelMaxSpeed;
    public float levelSpeedY;
    float baseAcceleration;
    float baseMaxSpeed;
    float baseSpeedY;

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

        var player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement_TestSalvador2>();
            playerHealth = player.GetComponent<Player_Health>();
            playerLanzaclavos = player.GetComponent<Player_Shoot>();
            playerLanzagranadas = player.GetComponent<Player_Shoot2>();
            playerLanzallamas = player.GetComponent<Player_Shoot3>();
            playerEscudoSierras = player.GetComponent<Player_Ability2>();
            playerEscudoReflector = player.GetComponent<Player_Ability_Fixed>();

            baseDamage = playerLanzaclavos.shootDamage;
            baseFireRate = playerLanzaclavos.fireRate;
            baseCannon = playerLanzaclavos.cannon;
            baseCannons = playerLanzaclavos.cannons;

            baseDamageLanzallamas = playerLanzallamas.shootDamage;
            baseFireRateLanzallamas = playerLanzallamas.fireRate;
            baseDistanceLanzallamas = playerLanzallamas.distance;
            baseMaxAngleLanzallamas = playerLanzallamas.maxAngle;

            baseDamageLanzagranadas = playerLanzagranadas.shootDamage;
            baseFireRateLanzagranadas = playerLanzagranadas.fireRate;

            baseEnergiaSierras = playerEscudoSierras.maxRechargeBar;
            baseSierrasNum = playerEscudoSierras.maxBullets;
            baseDamageSierras = playerEscudoSierras.damage;

            baseObjetivos = playerEscudoReflector.maxTargetNumber;
            baseAbsorcion = playerEscudoReflector.maxCharge;
            baseEnergiaReflector = playerEscudoReflector.maxRechargeBar;

            baseHealth = playerHealth.maxHealth;
            baseInvulnerability = playerHealth.invulnerabilityTime;

            baseAcceleration = playerMovement.accelerationX;
            baseMaxSpeed = playerMovement.maxSpeed;
            baseSpeedY = playerMovement.speedY;
        }

        UpdateUpgrades();

        SetUpgradesUI();
        SetEquipment();

        var branches = FindObjectsOfType<BranchNavigator>();

        for (int i = 0; i < branches.Length; i++)
        {
            branches[i].gameObject.SetActive(false);
        }

        GameObject.FindGameObjectWithTag("shopMenu").SetActive(false);
    }

    private void Update()
    {
        if (selectedWeaponUI != null) selectedWeaponUI.text = GetFeatureName(selectedWeapon);
        if (selectedShieldUI != null) selectedShieldUI.text = GetFeatureName(selectedShield);
    }

    public void UpdateUpgrades()
    {
        //Lanzaclavos
        int levelCanonDobleLanzaclavos = Array.Find(upgrades, (e) => e.feature == "LANZACLAVOS" && e.upgrade == "CANONDOBLE").level;
        int levelDanoLanzaclavos = Array.Find(upgrades, (e) => e.feature == "LANZACLAVOS" && e.upgrade == "DANO").level;
        int levelCadenciaLanzaclavos = Array.Find(upgrades, (e) => e.feature == "LANZACLAVOS" && e.upgrade == "CADENCIA").level;

        playerLanzaclavos.cannon = levelCanonDobleLanzaclavos == 0 ? baseCannon : level1Cannon;
        playerLanzaclavos.cannons = levelCanonDobleLanzaclavos == 0 ? baseCannons : level1Cannons;

        playerLanzaclavos.shootDamage = baseDamage * (levelDanoLanzaclavos + 1);
        playerLanzaclavos.fireRate = baseFireRate / (levelCadenciaLanzaclavos + 1);

        //Lanzallamas
        int levelAlcanceLanzallamas = Array.Find(upgrades, (e) => e.feature == "LANZALLAMAS" && e.upgrade == "ALCANCE").level;
        int levelRadioLanzallamas = Array.Find(upgrades, (e) => e.feature == "LANZALLAMAS" && e.upgrade == "RADIO").level;
        int levelDanoLanzallamas = Array.Find(upgrades, (e) => e.feature == "LANZALLAMAS" && e.upgrade == "DANO").level;
        int levelCadenciaLanzallamas = Array.Find(upgrades, (e) => e.feature == "LANZALLAMAS" && e.upgrade == "CADENCIA").level;

        playerLanzallamas.distance = levelAlcanceLanzallamas == 0 ? baseDistanceLanzallamas : level1Alcance;
        playerLanzallamas.maxAngle = levelRadioLanzallamas == 0 ? baseMaxAngleLanzallamas : level1Radio;

        playerLanzallamas.shootDamage = baseDamageLanzallamas * (levelDanoLanzallamas + 1);
        playerLanzallamas.fireRate = baseFireRateLanzallamas / (levelCadenciaLanzallamas + 1);

        //Lanzagranadas
        int levelCanonDobleLanzagranadas = Array.Find(upgrades, (e) => e.feature == "LANZAGRANADAS" && e.upgrade == "CANONDOBLE").level;
        int levelDanoLanzagranadas = Array.Find(upgrades, (e) => e.feature == "LANZAGRANADAS" && e.upgrade == "DANO").level;
        int levelCadenciaLanzagranadas = Array.Find(upgrades, (e) => e.feature == "LANZAGRANADAS" && e.upgrade == "CADENCIA").level;

        playerLanzagranadas.shootDamage = baseDamageLanzagranadas * (levelDanoLanzagranadas + 1);
        playerLanzagranadas.fireRate = baseFireRateLanzagranadas / (levelCadenciaLanzagranadas + 1);
        playerLanzagranadas.bulletCount = levelCanonDobleLanzagranadas + 1;

        //Escudo de Sierras
        int levelDanoSierras = Array.Find(upgrades, (e) => e.feature == "ESCUDOSIERRAS" && e.upgrade == "DANO").level;
        int levelEnergiaSierras = Array.Find(upgrades, (e) => e.feature == "ESCUDOSIERRAS" && e.upgrade == "ENERGIA").level;
        int levelCantidadSierras = Array.Find(upgrades, (e) => e.feature == "ESCUDOSIERRAS" && e.upgrade == "CANTIDAD").level;

        playerEscudoSierras.damage = baseDamageSierras * (levelDanoSierras + 1);
        playerEscudoSierras.SetRechargeBar(baseEnergiaSierras + TIME_PER_LEVEL_SHIELD * levelEnergiaSierras);
        playerEscudoSierras.maxBullets = baseSierrasNum + sierrasPorLevel * levelCantidadSierras;

        //Escudo Reflector
        int levelEnergiaReflector = Array.Find(upgrades, (e) => e.feature == "ESCUDOREFLECTOR" && e.upgrade == "ENERGIA").level;
        int levelObjetivosReflector = Array.Find(upgrades, (e) => e.feature == "ESCUDOREFLECTOR" && e.upgrade == "OBJETIVOS").level;
        int levelAbsorcionReflector = Array.Find(upgrades, (e) => e.feature == "ESCUDOREFLECTOR" && e.upgrade == "ABSORCION").level;

        playerEscudoReflector.maxCharge = baseAbsorcion * (levelAbsorcionReflector + 1);
        playerEscudoReflector.maxTargetNumber = baseObjetivos + objetivosPorLevel * levelObjetivosReflector;
        playerEscudoReflector.SetRechargeBar(baseEnergiaReflector + TIME_PER_LEVEL_SHIELD * levelEnergiaReflector);

        //Vindex Health
        int levelHealth = Array.Find(upgrades, (e) => e.feature == "VINDEX" && e.upgrade == "VIDA").level;
        int levelInvulnerability = Array.Find(upgrades, (e) => e.feature == "VINDEX" && e.upgrade == "INVULNERABILIDAD").level;

        playerHealth.SetHealth(baseHealth + healthPerLevel * levelHealth);
        playerHealth.invulnerabilityTime = baseInvulnerability + invulnerabilityPerLevel * levelInvulnerability;
    
        //Vindex Movement
        int levelSpeed = Array.Find(upgrades, (e) => e.feature == "VINDEX" && e.upgrade == "VELOCIDAD").level;

        playerMovement.accelerationX = levelSpeed == 0 ? baseAcceleration : levelAcceleration;
        playerMovement.maxSpeed = levelSpeed == 0 ? baseMaxSpeed : levelMaxSpeed;
        playerMovement.speedY = levelSpeed == 0 ? baseSpeedY : levelSpeedY;
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

    public void SetEquipment()
    {
        SectionNavigator sectionNav = FindObjectOfType<SectionNavigator>();
        sectionNav.selectedWeapon = selectedWeapon;
        sectionNav.selectedShield = selectedShield;

        playerLanzaclavos.equipped = false;
        playerLanzagranadas.equipped = false;
        playerLanzallamas.equipped = false;

        switch(selectedWeapon)
        {
            case "LANZACLAVOS":
                playerLanzaclavos.equipped = true;
                break;
            case "LANZAGRANADAS":
                playerLanzagranadas.equipped = true;
                break;
            case "LANZALLAMAS":
                playerLanzallamas.equipped = true;
                break;
        }

        playerEscudoSierras.equipped = false;
        playerEscudoReflector.equipped = false;

        switch(selectedShield)
        {
            case "ESCUDOSIERRAS":
                playerEscudoSierras.equipped = true;
                break;
            case "ESCUDOREFLECTOR":
                playerEscudoReflector.equipped = true;
                break;
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

    public string GetFeatureName(string feature)
    {
        switch (feature)
        {
            case "LANZACLAVOS":
                return "Lanzaclavos";
            case "LANZAGRANADAS":
                return "Lanzagranadas";
            case "LANZALLAMAS":
                return "Lanzallamas";

            case "ESCUDOSIERRAS":
                return "Escudo de Sierras";
            case "ESCUDOREFLECTOR":
                return "Escudo Reflector";

            case "VINDEX":
                return "Vindex";

            default:
                return "Feature Not Found";
        }
    }

    public int GetGearMultiplier() 
    {
        return Array.Find(upgrades, (e) => e.feature == selectedWeapon && e.upgrade == "IMAN").level + 1;
    }

    public int GetCoreMultiplier()
    {
        return Array.Find(upgrades, (e) => e.feature == "VINDEX" && e.upgrade == "NUCLEOS").level + 1;
    }
}
