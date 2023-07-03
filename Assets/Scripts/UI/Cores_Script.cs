using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cores_Script : MonoBehaviour
{
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip CoresSound;

    public void Start()
    {
        audioManager = GetComponent<AudioSource>();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        audioManager.PlayOneShot(CoresSound);
    //        GameScore.instance.AddCores(1);
    //        Destroy(gameObject);
    //    }
    //}
}
