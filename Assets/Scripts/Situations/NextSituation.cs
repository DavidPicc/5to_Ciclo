using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSituation : MonoBehaviour
{
    SituationManager situationManager;
    SituationManager_RE situationManagerRE;
    bool used = false;
    private void Start()
    {
        situationManager = GameObject.FindObjectOfType<SituationManager>();
        //situationManagerRE = GameObject.FindObjectOfType<SituationManager_RE>();
    }
    void InvokeSituation()
    {
        if (situationManager != null && situationManager.gameObject.activeSelf)
        {
            situationManager.SpawnSituation();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Activator") || other.CompareTag("Player")) && !used)
        {
            used = true;
            //Invoke("InvokeSituation", 0.2f);
            InvokeSituation();
        }
    }
}
