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
    bool passedPlayer = false;
    Vector2 lastMovement;

    public Animator animIdle;
    bool enemyIdle = true;
    public Animator animAttack;
    bool enemyAttack = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveToPlayer();

        //animIdle.SetBool("Idle", enemyIdle);
    }

    void MoveToPlayer()
    {
        //Debug.Log(transform.position.x - player.transform.position.x);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        if(passedPlayer)
        {
            rb.velocity = lastMovement * speed/1.5f;
        }
        else
        {
            if (transform.position.x - player.transform.position.x >= 2f)
            {
                rb.velocity = direction * speed;
            }
            else
            {
                passedPlayer = true;
                lastMovement = direction;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyAttack = true;
            enemyIdle = false;
            //animAttack.SetBool("Attack", enemyAttack);
        }
    }
}
