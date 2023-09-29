using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSituation : MonoBehaviour
{
    SituationManager situationManager;
    SituationManager_RE situationManagerRE;
    private void Start()
    {
        situationManager = GameObject.FindObjectOfType<SituationManager>();
        situationManagerRE = GameObject.FindObjectOfType<SituationManager_RE>();
    }
    void InvokeSituation()
    {

        //SituationManager.instance.SpawnSituation();
        //SituationManager_RE.instance.SpawnSituation();
        if (situationManager.gameObject.activeSelf)
        {
            situationManagerRE.ActivateSituation();
            situationManager.SpawnSituation();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Activator"))
        {
            Invoke("InvokeSituation", 0.2f);
        }
    }
}
