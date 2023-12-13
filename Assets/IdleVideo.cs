using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdleVideo : MonoBehaviour
{
    public float cronometro = 0;
    // Start is called before the first frame update
    void Start()
    {
        cronometro = 30;
    }

    // Update is called once per frame
    void Update()
    {
        cronometro -= 1 * Time.deltaTime;

        if (Input.anyKeyDown)
        {
            cronometro = 30;
        }
            
        if (cronometro <= 0)
        {
            SceneManager.LoadScene("VideoIdle");
        }
    }
}
