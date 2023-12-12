using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoReturn : MonoBehaviour
{
    public float timer;
    void Start()
    {
        Invoke("ChangeScene", timer);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Test_Menu");
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Test_Menu"); 
    }
}
