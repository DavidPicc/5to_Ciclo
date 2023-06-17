using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VolumeButtonScript : MonoBehaviour
{
    public GameObject volumeButton, leftButton, rightButton;
    public TextMeshProUGUI volumeText;

    public void ActivateButtons()
    {
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        volumeText.gameObject.SetActive(true);
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
    }
}
