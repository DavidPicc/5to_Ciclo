using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject shopMenu;
    public GameObject nextLevelTransition;
    public bool isPaused = false;
    public string nextLevelName;

    public delegate void ShopApply();
    public static event ShopApply onShopApply;

    [SerializeField] AudioClip levelMusic;

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
            if (!deathMenu.activeSelf)
            {
                if (!isPaused)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            FinishedLevel();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (shopMenu.activeSelf) CloseShop();
    }

    public void DeathMenu()
    {
        deathMenu.SetActive(true);
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
        shopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        Time.timeScale = 1f;
        shopMenu.SetActive(false);
        //CheckPointScript.instance.UpdateCheckpoints();

        if(onShopApply != null) onShopApply();
    }

    public void FinishedLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player_Health>().enabled = false;
        player.GetComponent<Player_Movement>().enabled = false;
        player.GetComponent<Player_Shoot>().enabled = false;
        player.GetComponent<Collider>().enabled = false;
        nextLevelTransition.SetActive(true);
        Debug.Log("SIGUIENTE NIVEL DESBLOQUEADO");
    }
}
