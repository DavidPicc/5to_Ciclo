using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detec_player : MonoBehaviour
{
    public GameObject obj;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            obj.SetActive(true);
            Destroy(gameObject,4f);
        }
    }
}

