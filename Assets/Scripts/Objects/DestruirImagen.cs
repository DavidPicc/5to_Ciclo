using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirImagen : MonoBehaviour
{
    [SerializeField] float time = 1f;
    private GameObject activador;

    void Start()
    {
        activador = GameObject.FindGameObjectWithTag("activadorImagen");
        Destroy(activador, time);
    }
}
