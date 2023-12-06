using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Transform player;
    [SerializeField] int levelNumber;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    [SerializeField] AudioClip DeathMusic;
    public GameObject shopMenu;
    public GameObject defaultCamera;
    public GameObject shopCamera;
    public GameObject nextLevelTransition;
    public GameObject shopTransition;
    public bool isPaused = false;
    public bool canUseAbilities = true;
    bool canPause = true;
    public string nextLevelName;

    public delegate void ShopApply();
    public static event ShopApply onShopApply;

    [SerializeField] AudioClip levelMusic;
    [SerializeField] Button deathButton;
    public SituationManager Waves;

    public bool close = false;

    void Awake()
    {
        instance = this;
        Waves = FindAnyObjectByType<SituationManager>();
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
        pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu");
        deathMenu = GameObject.FindGameObjectWithTag("deathMenu");

        deathMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    void Start()
    {
        player = FindObjectOfType<Player_Health>().transform;
        AudioManager.instance.ChangeMusic(levelMusic);
        ResumeGame();
        deathMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canPause && !deathMenu.activeSelf && !isPaused)
            {
                PauseGame();
            }
        }

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    FinishedLevel();
        //}

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    SceneManager.LoadScene("Test_menu");
        //}
    }

    public void UseAbilitesAfterTime()
    {
        canUseAbilities = true;
    }

    public void CanPauseAfterTime()
    {
        canPause = true;
    }

    float oldSFXVolume = 0;
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        //Debug.Log("PAUSA");
        AudioManager.masterVolume /= 2;
        AudioManager.instance.inGameSFX.audioMixer.SetFloat("InGameSFXVolume", -80f);
        AudioManager.instance.menuSFX.audioMixer.SetFloat("MenuSFXVolume", 0f);
        //oldSFXVolume = AudioManager.sfxVolume;
        //AudioManager.sfxVolume = 0;
        //AudioManager.instance.UpdateMixerVolume();
        //FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 0.5f;

        Time.timeScale = 0f;
        isPaused = true;
        canUseAbilities = false;
        canPause = false;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        AudioManager.masterVolume *= 2;
        AudioManager.instance.inGameSFX.audioMixer.SetFloat("InGameSFXVolume", 0f);
        AudioManager.instance.menuSFX.audioMixer.SetFloat("MenuSFXVolume", -80f);
        //AudioManager.sfxVolume = oldSFXVolume;
        //AudioManager.instance.UpdateMixerVolume();
        //FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 1f;

        if (shopMenu.activeSelf) CloseShop();

        Invoke("UseAbilitesAfterTime", 1f);
        Invoke("CanPauseAfterTime", 1f);
    }

    public void DeathMenu()
    {
        AudioManager.instance.ChangeMusic(DeathMusic);
        FindObjectOfType<EventSystem>().SetSelectedGameObject(deathButton.gameObject);
        deathMenu.SetActive(true);
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 0.5f;
        Time.timeScale = 0f;
    }

    public void LoadScene(string sceneName)
    {
        ResumeGame();
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        ResumeGame();
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenShop()
    {
        shopTransition.SetActive(true);
        shopTransition.GetComponent<Animator>().SetTrigger("OpenShop");

        AudioManager.instance.inGameSFX.audioMixer.SetFloat("InGameSFXVolume", -80f);
        AudioManager.instance.menuSFX.audioMixer.SetFloat("MenuSFXVolume", 0f);


        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 0.5f;

        canUseAbilities = false;
        canPause = false;

        CheckPointScript.savedPlayerX = player.localPosition.x;
        CheckPointScript.savedPlayerY = player.localPosition.y;
        PlayerPrefs.SetFloat("savedPlayerX", CheckPointScript.savedPlayerX);
        PlayerPrefs.SetFloat("savedPlayerY", CheckPointScript.savedPlayerY);
    }

    public void OpenShopAfterTransition()
    {
        //player.localPosition = new Vector3(-2, 0, 0);
        //FindObjectOfType<StageMovement>().transform.position -= Vector3.right * 7f;
        player.localPosition = Vector3.zero;

        if (defaultCamera != null) defaultCamera.SetActive(false);
        if (shopCamera != null) shopCamera.SetActive(true);

        isPaused = true;
        shopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        AudioManager.instance.inGameSFX.audioMixer.SetFloat("InGameSFXVolume", 0f);
        AudioManager.instance.menuSFX.audioMixer.SetFloat("MenuSFXVolume", -80f);

        if (shopCamera != null) shopCamera.SetActive(false);
        if (defaultCamera != null) defaultCamera.SetActive(true);

        if(UpgradeTrackerNewShop.instance != null)
        {
            UpgradeTrackerNewShop.instance.UpdateUpgrades();
            StartCoroutine(UpgradeTrackerNewShop.instance.SetEquipment());
        }


        Time.timeScale = 1f;
        shopMenu.SetActive(false);
        shopTransition.SetActive(false);
        //CheckPointScript.instance.UpdateCheckpoints();
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 1f;

        if (onShopApply != null) onShopApply();

        Invoke("UseAbilitesAfterTime", 1f);
        Invoke("CanPauseAfterTime", 1f);

        close = true;
    }

    public void FinishedLevel()
    {
        GameObject player = FindObjectOfType<Player_Health>().gameObject;
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 1f;
        player.GetComponent<Player_Health>().enabled = false;
        player.GetComponent<Player_Movement>().enabled = false;
        player.GetComponent<Player_Shoot>().enabled = false;
        player.GetComponent<Collider>().enabled = false;
        nextLevelTransition.SetActive(true);
        if (FindObjectOfType<LevelUnlock>() != null) LevelUnlock.Instance.UpdateUnlockedLevels(levelNumber);
        //Debug.Log("SIGUIENTE NIVEL DESBLOQUEADO");
        if (FindObjectOfType<CheckPointScript>() != null) FindObjectOfType<CheckPointScript>().ResetCheckpoints();
    }

    public void StopWaves()
    {
        Waves.enabled = false;
    }

    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        if(FindObjectOfType<LevelUnlock>() != null) PlayerPrefs.SetInt("LevelsUnlocked", LevelUnlock.Instance.levels);
    }
}
