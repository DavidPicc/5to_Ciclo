using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movimiento en línea recta
public class Enemy1_Movement : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
