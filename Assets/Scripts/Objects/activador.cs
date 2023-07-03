using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activador : MonoBehaviour
{
    public GameObject objectToActivate1;
   // public GameObject objectToActivate2;
    private bool secondObjectActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bosstest"))
        {
            objectToActivate1.SetActive(true);
           // Invoke("ActivateSecondObject", 0.5f);
        }
    }

    //private void ActivateSecondObject()
    //{
    //    if (!secondObjectActivated)
    //    {
    //        objectToActivate2.SetActive(true);
    //        secondObjectActivated = true;
    //    }
    //}
}
