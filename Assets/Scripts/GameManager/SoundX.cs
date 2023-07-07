using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundX : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioManager manager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            manager.PlaySFXOnce(source, clip, 3f);
        }
    }
}
