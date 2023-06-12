using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void LoadNextLevel(string levelName)
    {
        levelName = GameManager.instance.nextLevelName;
        if(FindObjectOfType<CheckPointScript>() != null )
        {
            CheckPointScript.instance.ResetCheckpoints();
        }
        SceneManager.LoadScene(levelName);
    }
}
