using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VolumeButtonScript : MonoBehaviour
{
    public GameObject volumeButton, leftButton, rightButton;
    public TextMeshProUGUI volumeText;
    public static string lastText;

    public enum VolumeType
    {
        general,
        music,
        sfx
    }
    public VolumeType volumeType;
    public void ActivateButtons()
    {
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        volumeText.gameObject.SetActive(true);

        switch (volumeType)
        {
            case VolumeType.general:
                volumeText.text = (AudioManager.masterVolume * 10f).ToString("F0");
                break;
            case VolumeType.music:
                volumeText.text = (AudioManager.musicVolume * 10f).ToString("F0");
                break;
            case VolumeType.sfx:
                volumeText.text = (AudioManager.sfxVolume * 10f).ToString("F0");
                break;
        }

        leftButton.GetComponent<Button>().Select();
        FindObjectOfType<UIPriority>().volumeSelected = this.gameObject;
    }

    public void DeactivateButtons()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        volumeText.gameObject.SetActive(false);
        FindObjectOfType<UIPriority>().volumeSelected = null;
        volumeButton.GetComponent<Button>().Select();
        //lastText = volumeText.text;
    }
}
