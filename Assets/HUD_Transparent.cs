using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Transparent : MonoBehaviour
{
    Transform player;
    public Image[] HUDImages;
    public TextMeshProUGUI[] texts;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(HUDImages.Length > 0)
            {
                foreach (Image image in HUDImages)
                {
                    Color currentColor = image.color;
                    currentColor.a = 0.2f;
                    image.color = currentColor;
                }
            }
            if(texts.Length > 0)
            {
                foreach (TextMeshProUGUI text in texts)
                {
                    Color currentColor = text.color;
                    currentColor.a = 0.2f;
                    text.color = currentColor;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HUDImages.Length > 0)
            {
                foreach (Image image in HUDImages)
                {
                    Color currentColor = image.color;
                    currentColor.a = 1f;
                    image.color = currentColor;
                }
            }
            if (texts.Length > 0)
            {
                foreach (TextMeshProUGUI text in texts)
                {
                    Color currentColor = text.color;
                    currentColor.a = 1f;
                    text.color = currentColor;
                }
            }
        }
    }
}
