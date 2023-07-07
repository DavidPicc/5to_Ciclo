using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClickZ : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioManager manager;
    public GameManager game;

    void Update()
    {
        if (game.isPaused)
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {
                manager.PlaySFXOnce(source, clip, 3f);
            }
            
        }


    }
}
