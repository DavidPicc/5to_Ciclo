using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarImagenEnColision : MonoBehaviour
{
    [SerializeField] GameObject imagenActivar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivarDesactivarImagen();
        }
    }
    private void ActivarDesactivarImagen()
    {
        imagenActivar.SetActive(!imagenActivar.activeSelf);
    }
}
