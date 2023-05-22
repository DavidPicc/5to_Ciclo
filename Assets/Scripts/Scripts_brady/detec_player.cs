using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detec_player : MonoBehaviour
{
    public GameObject obj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            obj.SetActive(true);  
        }
    }
}

