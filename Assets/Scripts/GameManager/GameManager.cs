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
    public bool isPaused = false;

    public delegate void ShopApply();
    public static event ShopApply onShopApply;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ResumeGame();
        deathMenu.SetActive(false);
        shopMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
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
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenShop()
    {
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
}
