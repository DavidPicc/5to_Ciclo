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
    public GameObject shopMenu;
    public GameObject nextLevelTransition;
    public bool isPaused = false;
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
            if (!deathMenu.activeSelf)
            {
                if (!isPaused)
                {
                    PauseGame();
                }
                //else
                //{
                //    ResumeGame();
                //}
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

    public void PauseGame()
    {
        pauseMenu.SetActive(true);

        //AudioManager.masterVolume /= 2;
        //AudioManager.instance.UpdateMixerVolume();
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 0.5f;

        Time.timeScale = 0f;
        isPaused = true;
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
    }

    public void DeathMenu()
    {
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
    }

    public void CloseShop()
    {
        Time.timeScale = 1f;
        shopMenu.SetActive(false);
        //CheckPointScript.instance.UpdateCheckpoints();
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioSource>().volume = 1f;

        if (onShopApply != null) onShopApply();
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
