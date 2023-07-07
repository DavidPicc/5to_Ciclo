using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundZ : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioManager manager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            manager.PlaySFXOnce(source, clip, 1f);
        }
    }
}
