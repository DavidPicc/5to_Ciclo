using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSituation : MonoBehaviour
{
    void InvokeSituation()
    {
        SituationManager.instance.SpawnSituation();
        //SituationManager_RE.instance.SpawnSituation();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Invoke("InvokeSituation", 0.2f);
        }
    }
}
