using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        if (FindObjectOfType<LevelUnlock>() != null) PlayerPrefs.SetInt("LevelsUnlocked", LevelUnlock.Instance.levels);
    }
}
