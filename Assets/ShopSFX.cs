using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buySFX, declineSFX, equipSFX, navSFX, backSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BuySFX()
    {
        AudioManager.instance.PlaySFX(audioSource, buySFX, 1f);
    }

    public void DeclineSFX()
    {
        AudioManager.instance.PlaySFX(audioSource, declineSFX, 1f);
    }

    public void EquipSFX()
    {
        AudioManager.instance.PlaySFX(audioSource, equipSFX, 1f);
    }

    public void NavSFX()
    {
        AudioManager.instance.PlaySFX(audioSource, navSFX, 1f);
    }

    public void BackSFX()
    {
        AudioManager.instance.PlaySFX(audioSource, backSFX, 1f);
    }
}
