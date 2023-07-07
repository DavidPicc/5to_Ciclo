using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioManager manager;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            manager.PlaySFXOnce(source, clip, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            manager.PlaySFXOnce(source, clip, 0.5f);
        }
    }

  
}
