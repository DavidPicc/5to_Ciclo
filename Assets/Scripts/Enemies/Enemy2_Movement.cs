using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

// Movimiento en linea recta hasta cierto punto, luego pasa a quedarse quieto.
public class Enemy2_Movement : MonoBehaviour
{
    public Transform _camera;
    public Transform player;
    public Rigidbody rb;

    [Header("Movement")]
    public float speed;
    public float xLock;
    bool locked = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = FindObjectOfType<StageMovement>().transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveAndStop();
    }

    void MoveAndStop()
    {
        Vector3 _camVec = new Vector3(_camera.position.x, 0, 0);
        Vector3 enemyVec = new Vector3(transform.position.x, 0, 0);

        if (Vector3.Distance(_camVec, enemyVec) <= xLock)
        {
            locked = true;
        }
        //else
        //{
        //    rb.velocity = transform.right * speed;
        //}

        if (locked)
        {
            transform.parent = _camera;
            rb.velocity = Vector3.zero;
        }
    }
}
