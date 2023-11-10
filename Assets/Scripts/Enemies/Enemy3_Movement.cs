using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movimiento rotatorio.
public class Enemy3_Movement : EnemyMovement
{
    Rigidbody rb;
    [SerializeField] float rotationSpeed;

    public Animator animIdle;
    bool enemyIdle = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //GetComponent<Enemy3_Shoot>().shootPoint.Rotate(0,0, rotationSpeed * Time.deltaTime);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        //animIdle.SetBool("Idle", enemyIdle);
    }
}
