using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoReturn : MonoBehaviour
{
    public float timer;
    public bool press;
    public string SceneName;
    void Start()
    {
        Invoke("ChangeScene", timer);
    }

    void Update()
    {
        if (Input.anyKeyDown && press == true || Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneName); 
    }
}
