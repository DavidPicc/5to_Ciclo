using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarRigidbodyAlEntrarEnTrigger : MonoBehaviour
{
    public string tagDelJugador = "Player";
    public GameObject objetoConRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagDelJugador))
        {
            ActivarRigidbody();
        }
    }

    private void ActivarRigidbody()
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
                //Debug.LogWarning("El objeto asignado no tiene un componente Rigidbody.");
            }
        }
    }
}
