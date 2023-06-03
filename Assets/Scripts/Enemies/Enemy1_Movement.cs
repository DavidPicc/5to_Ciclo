using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movimiento en línea recta
public class Enemy1_Movement : EnemyMovement
{
    Rigidbody rb;
    [Header("Movement")]
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = transform.right * speed;
    }
}
