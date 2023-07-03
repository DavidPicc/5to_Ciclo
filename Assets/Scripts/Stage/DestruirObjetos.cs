using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObjetos : MonoBehaviour
{

    public string tagObjetoADestruir = "stage";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObjetoADestruir))
        {
            Destroy(other.gameObject);
        }
    }
}
