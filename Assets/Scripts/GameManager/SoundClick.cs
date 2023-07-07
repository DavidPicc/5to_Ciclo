using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClick : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioManager manager;
    public GameManager game;

    void Update()
    {
        if (game.isPaused)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                manager.PlaySFXOnce(source, clip, 0.5f);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                manager.PlaySFXOnce(source, clip, 0.5f);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                manager.PlaySFXOnce(source, clip, 0.5f);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                manager.PlaySFXOnce(source, clip, 0.5f);
            }
        }
       

    }
}
