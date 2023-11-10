using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movimiento en l�nea recta
public class Enemy1_Movement : EnemyMovement
{
    Rigidbody rb;
    [Header("Movement")]
    public float speed;
    public Animator anim;
    bool enemigoEnIdle = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = transform.right * speed;
        //anim.SetBool("Attack", enemigoEnIdle);
    }
}
