using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cores_Script : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameScore.instance.AddCores(1);
            Destroy(gameObject);
        }
    }
}
