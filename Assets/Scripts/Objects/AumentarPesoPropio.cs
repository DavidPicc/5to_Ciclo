using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentarPesoPropio : MonoBehaviour
{
    public float factorDeAumento = 2f; // Ajusta este valor para cambiar la fuerza de caída

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            //Debug.LogError("El objeto al que se aplica este script no tiene un Rigidbody.");
        }
    }

    private void Update()
    {
        if (rb != null)
        {
            Physics.gravity = new Vector3(0, -9.81f * factorDeAumento, 0);
        }
    }
}
