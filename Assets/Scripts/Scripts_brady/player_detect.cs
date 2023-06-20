using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_detect : MonoBehaviour
{
    public GameObject chain;
    public GameObject chain2;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag=="Player")
        {
            Debug.Log("Aca");
            StartCoroutine(falling());
            chain.SetActive(false);
            Destroy(GetComponent<Collider>());
        }
      
    }
    IEnumerator falling()
    {
        while (true)
        {
            chain2.GetComponent<Rigidbody>().AddForce(new Vector3(-100, 0, 0));
            yield return null;
        }
    }
}

