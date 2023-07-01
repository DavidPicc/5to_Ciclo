using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    [SerializeField] AudioClip DeathMusic;
    public GameObject shopMenu;
    public GameObject nextLevelTransition;
    public bool isPaused = false;
    public bool canUseAbilities = true;
    bool canPause = true;
    public string nextLevelName;

    public delegate void ShopApply();
    public static event ShopApply onShopApply;

    [SerializeField] AudioClip levelMusic;
    [SerializeField] Button deathButton;

    void Awake()
    {
        instance = this;
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

        if (Input.GetKeyDown(KeyCode.U))
        {
            FinishedLevel();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Test_menu");
        }
    }

    public void UseAbilitesAfterTime()
    {
        canUseAbilities = true;
    }

    public void CanPauseAfterTime()
    {
        canPause = true;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Debug.Log("PAUSA");
        //AudioManager.masterVolume /= 2;
        //AudioManager.instance.UpdateMixerVolume();
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 0.5f;

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

        //AudioManager.masterVolume *= 2;
        //AudioManager.instance.UpdateMixerVolume();
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 1f;

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
        isPaused = true;

        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 0.5f;
        shopMenu.SetActive(true);

        canUseAbilities = false;
        canPause = false;
    }

    public void CloseShop()
    {
        Time.timeScale = 1f;
        shopMenu.SetActive(false);
        //CheckPointScript.instance.UpdateCheckpoints();
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 1f;

        if (onShopApply != null) onShopApply();

        Invoke("UseAbilitesAfterTime", 1f);
        Invoke("CanPauseAfterTime", 1f);
    }

    public void FinishedLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 1f;
        player.GetComponent<Player_Health>().enabled = false;
        player.GetComponent<Player_Movement>().enabled = false;
        player.GetComponent<Player_Shoot>().enabled = false;
        player.GetComponent<Collider>().enabled = false;
        nextLevelTransition.SetActive(true);
        Debug.Log("SIGUIENTE NIVEL DESBLOQUEADO");
    }
}
