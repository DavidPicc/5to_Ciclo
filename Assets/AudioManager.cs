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
    public float masterVolume;
    public AudioMixerGroup musicGroup;
    //public Slider musicVolumeSlider;
    public float musicVolume;
    public AudioMixerGroup sfxGroup;
    //public Slider sfxVolumeSlider;
    public float sfxVolume;

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
        musicSource = GetComponent<AudioSource>();
        //musicClip = (AudioClip)Resources.Load("Audio/Music/testAudio");
        //shotSFX = (AudioClip)Resources.Load("Audio/SFX/billSound");
        //endRoundSFX = (AudioClip)Resources.Load("Audio/SFX/endRoundSFX");
        //satanVoice = (AudioClip)Resources.Load("Audio/SFX/satanVoice");

        //ChangeMusic(musicClips);

        // PARA BAJAR EL VOLUMEN PORQUE ESTÁ MU ALTO.
        //masterVolumeSlider.value = 0.5f;
        //musicVolumeSlider.value = 1f;
        //sfxVolumeSlider.value = 1f;

        masterVolume = 0.5f;
        musicVolume = 0.5f;
        sfxVolume = 0.5f;

        UpdateMixerVolume();
    }

    void AddArrayToList(List<AudioClip> clipsGame, AudioClip[] clips)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            clipsGame.Add(clips[i]);
        }
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
        masterVolume += value;
        masterVolume = Mathf.Clamp(masterVolume, 0.0001f, 1f);
        masterGroup.audioMixer.SetFloat("GeneralVolume", Mathf.Log10(masterVolume) * 20);

        FindObjectOfType<UIPriority>().volumeSelected.GetComponent<VolumeButtonScript>().volumeText.text = (masterVolume * 10f).ToString("F0");
    }

    public void ChangeMusicVolume(float value)
    {
        musicVolume += value;
        musicVolume = Mathf.Clamp(musicVolume, 0.0001f, 1f);
        musicGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);

        FindObjectOfType<UIPriority>().volumeSelected.GetComponent<VolumeButtonScript>().volumeText.text = (musicVolume * 10f).ToString("F0");
    }

    public void ChangeSFXVolume(float value)
    {
        sfxVolume += value;
        sfxVolume = Mathf.Clamp(sfxVolume, 0.0001f, 1f);
        sfxGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);

        FindObjectOfType<UIPriority>().volumeSelected.GetComponent<VolumeButtonScript>().volumeText.text = (sfxVolume * 10f).ToString("F0");
    }
}
