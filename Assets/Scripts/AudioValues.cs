using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioValues : MonoBehaviour
{
    public static AudioValues instance;
    public static float _masterVolume;
    public static float _musicVolume;
    public static float _sfxVolume;

    public bool isMainMenu = false;

    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (instance == null)
        {
            instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (isMainMenu)
        {
            _masterVolume = 0.5f;
            _musicVolume = 0.5f;
            _sfxVolume = 0.5f;
        }
    }
}
