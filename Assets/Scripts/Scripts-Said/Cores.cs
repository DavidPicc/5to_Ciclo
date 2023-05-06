using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cores : MonoBehaviour
{
    [SerializeField] private float coresAmount;
    [SerializeField] private CorePoints corep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            corep.AddCore(coresAmount);
        }
    }
}
