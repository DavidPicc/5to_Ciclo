using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cores_Script : MonoBehaviour
{
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip CoresSound;
    [SerializeField] MeshRenderer coreRenderer;
    bool done = false;

    public void Start()
    {
        audioManager = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !done)
        {
            audioManager.PlayOneShot(CoresSound);

            float multiplier = UpgradeTrackerNewShop.instance != null ? UpgradeTrackerNewShop.instance.GetCoreMultiplier() : 1;

            GameScore.instance.AddCores(Mathf.FloorToInt(multiplier));
            coreRenderer.enabled = false;
            Destroy(gameObject,1f);
            done = true;
        }
    }
}
