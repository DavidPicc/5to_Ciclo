using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRotate : MonoBehaviour
{
    public Rotate rotate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rotate.playerInProximity = true;
        }
    }
}
