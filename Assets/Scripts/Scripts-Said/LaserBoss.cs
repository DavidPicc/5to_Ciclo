using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBoss : MonoBehaviour
{
    public GameObject[] objectsToActivate; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
    }
}
