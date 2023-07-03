using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_nextlevel_2 : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("MainMenuPrueba", LoadSceneMode.Single);
        }
    }
}
