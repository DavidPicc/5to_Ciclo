using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource;
    //public AudioSource sfxSource;

    [Header("Audio Mixer")]
    public AudioMixerGroup masterGroup;
    //public Slider masterVolumeSlider;
    public static float masterVolume;
    public AudioMixerGroup musicGroup;
    //public Slider musicVolumeSlider;
    public static float musicVolume;
    public AudioMixerGroup sfxGroup;
    //public Slider sfxVolumeSlider;
    public static float sfxVolume;

    public bool isMainMenu = false;
    public AudioClip screenMusic;

    private void Awake()
    {
        instance = this;

        //// If there is not already an instance of SoundManager, set it to this.
        //if (instance == null)
        //{
        //    instance = this;
        //}
        ////If an instance already exists, destroy whatever this object is to enforce the singleton.
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}

        ////Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        //if(isMainMenu)
        //{
        //    masterVolume = 0.5f;
        //    musicVolume = 0.5f;
        //    sfxVolume = 0.5f;
        //}
        masterVolume = AudioValues._masterVolume;
        musicVolume = AudioValues._musicVolume;
        sfxVolume = AudioValues._sfxVolume;

        if(screenMusic != null)   
            ChangeMusic(screenMusic);
        UpdateMixerVolume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(AudioSource source, AudioClip clip, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    public void PlaySFXWithDelay(AudioSource source, AudioClip clip, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.PlayDelayed(0.5f);
    }

    public void PlaySFXOnce(AudioSource source, AudioClip clip, float volume)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.clip = clip;
        source.volume = volume;
        source.PlayOneShot(clip);
    }

    public void ChangeMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void UpdateMixerVolume()
    {
        //masterVolume = masterVolumeSlider.value;
        //musicVolume = musicVolumeSlider.value;
        //sfxVolume = sfxVolumeSlider.value;

        masterGroup.audioMixer.SetFloat("GeneralVolume", Mathf.Log10(masterVolume) * 20);
        musicGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        sfxGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioValues._masterVolume += value;
        AudioValues._masterVolume = Mathf.Clamp(AudioValues._masterVolume, 0.0001f, 1f);
        masterVolume = AudioValues._masterVolume;
        masterGroup.audioMixer.SetFloat("GeneralVolume", Mathf.Log10(masterVolume) * 20);

        FindObjectOfType<UIPriority>().volumeSelected.GetComponent<VolumeButtonScript>().volumeText.text = (masterVolume * 10f).ToString("F0");
    }

    public void ChangeMusicVolume(float value)
    {
        AudioValues._musicVolume += value;
        AudioValues._musicVolume = Mathf.Clamp(AudioValues._musicVolume, 0.0001f, 1f);
        musicVolume = AudioValues._musicVolume;
        musicGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);

        FindObjectOfType<UIPriority>().volumeSelected.GetComponent<VolumeButtonScript>().volumeText.text = (musicVolume * 10f).ToString("F0");
    }

    public void ChangeSFXVolume(float value)
    {
        AudioValues._sfxVolume += value;
        AudioValues._sfxVolume = Mathf.Clamp(AudioValues._sfxVolume, 0.0001f, 1f);
        sfxVolume = AudioValues._sfxVolume;
        sfxGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);

        FindObjectOfType<UIPriority>().volumeSelected.GetComponent<VolumeButtonScript>().volumeText.text = (sfxVolume * 10f).ToString("F0");
    }

    //public void ChangeSFXVolume(float value)
    //{
    //    sfxVolume += value;
    //    sfxVolume = Mathf.Clamp(sfxVolume, 0.0001f, 1f);
    //    sfxGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);

    //    FindObjectOfType<UIPriority>().volumeSelected.GetComponent<VolumeButtonScript>().volumeText.text = (sfxVolume * 10f).ToString("F0");
    //}
}
