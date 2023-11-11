using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public GameObject objetoConRigidbody;

    private void OnDestroy()
    {
        if (objetoConRigidbody != null)
        {
            Rigidbody rb = objetoConRigidbody.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            else
            {
                Debug.LogWarning("El objeto asignado no tiene un componente Rigidbody.");
            }
        }
    }
}
