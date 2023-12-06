using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_detect : MonoBehaviour
{
    public GameObject chain;
    public bool chainF;
    public float timer;
    public float maxTimer = 3f;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag=="Player")
        {
            //Debug.Log("Aca");
            timer += Time.deltaTime;
            StartCoroutine(falling());
            chain.SetActive(false);
            chainF = true;
            if (chainF && timer >= maxTimer)
            {
                Destroy(gameObject);
            }
        }
      
    }
    IEnumerator falling()
    {
        while (true)
        {
            yield return null;
        }
    }
}

