using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movimiento dirigido al jugador.
public class Enemy4_Movement : EnemyMovement
{
    public Transform _camera;
    public Transform player;
    public Rigidbody rb;

    [Header("Movement")]
    public float speed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
}
