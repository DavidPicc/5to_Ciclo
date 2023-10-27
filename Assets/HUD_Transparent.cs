using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Transparent : MonoBehaviour
{
    Transform player;
    public Image[] HUDImages;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(Image image in HUDImages)
            {
                Color currentColor = image.color;
                currentColor.a = 0.4f;
                image.color = currentColor;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Image image in HUDImages)
            {
                Color currentColor = image.color;
                currentColor.a = 1f;
                image.color = currentColor;
            }
        }
    }
}
