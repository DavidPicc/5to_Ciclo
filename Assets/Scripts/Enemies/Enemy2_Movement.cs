using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

// Movimiento en linea recta hasta cierto punto, luego pasa a quedarse quieto.
public class Enemy2_Movement : EnemyMovement
{
    public Transform _camera;
    public Transform player;
    public Rigidbody rb;

    [Header("Movement")]
    public float speed;
    public float xLock;
    public bool locked = false;
    public bool timeToGo = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = FindObjectOfType<StageMovement>().transform;
        rb = GetComponent<Rigidbody>();
        timeToGo = false;
    }

    void Update()
    {
        MoveAndStop();
    }

    void MoveAndStop()
    {
        Vector3 _camVec = new Vector3(_camera.position.x, 0, 0);
        Vector3 enemyVec = new Vector3(transform.position.x, 0, 0);

        if(!locked)
        {
            transform.parent = null;
            if(!timeToGo)
            {
                if (Vector3.Distance(_camVec, enemyVec) <= xLock)
                {
                    locked = true;
                }
            }
        }
        else
        {
            transform.parent = _camera;
            rb.velocity = Vector3.zero;
        }

        //else
        //{
        //    rb.velocity = transform.right * speed;
        //}
    }
}
