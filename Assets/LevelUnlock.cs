using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUnlock : MonoBehaviour
{
    public static LevelUnlock Instance;
    public int levels;
    public bool loadedUnlockedLevel;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this GameObject from being destroyed
        }
        else
        {
            Destroy(gameObject); // Destroy any additional instances created
        }
        levels = PlayerPrefs.GetInt("LevelsUnlocked", 0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            //Debug.Log("Need money? " + loadedUnlockedLevel);
            //Debug.Log("Levels: " + levels);
        }
    }

    public void CheckUnlockedLevels()
    {
        loadedUnlockedLevel = false;
        levels = PlayerPrefs.GetInt("LevelsUnlocked", 0);
    }

    public void UnlockLevels()
    {
        if (FindObjectOfType<LevelSelector>() != null)
        {
            LevelSelector selector = FindObjectOfType<LevelSelector>();
            for (int i = 0; i < levels; i++)
            {
                selector.levelsButtons[i].interactable = true;
                Color transparency = selector.levelsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color;
                transparency.a = 1f;
                selector.levelsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = transparency;
            }
        }
    }

    public void UpdateUnlockedLevels(int integer)
    {
        if(levels <= integer) levels = integer;
        PlayerPrefs.SetInt("LevelsUnlocked", levels);
        //Debug.Log("GUARDADOS TUS NIVELES, CRACK. Nivel: " + levels);
    }
}
