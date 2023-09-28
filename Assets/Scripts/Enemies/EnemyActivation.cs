using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    GameObject player;
    Transform _camera;

    public EnemyMovement movementScript;
    public EnemyShooting enemyShooting;
    public Enemy_Health enemyHealth;

    public float checkTimer = 2f;
    float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _camera = GameObject.FindObjectOfType<StageMovement>().transform;

        if (movementScript != null)
            movementScript.enabled = false;

        if (enemyShooting != null)
            enemyShooting.enabled = false;

       // if (enemyHealth != null)
       //     enemyHealth.enabled = false;

        timer = 0;

        if (Mathf.Abs(transform.position.y) - Mathf.Abs(_camera.transform.position.y) > 30f)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //timer -= Time.deltaTime;
        //if (timer <= 0)
        //{
        //    if(Vector3.Distance(player.transform.position, transform.position) <= 50f)
        //    {
        //        ActivateEnemy();
        //    }
        //    timer = checkTimer;
        //}
        if (Mathf.Abs(transform.position.x) - Mathf.Abs(_camera.transform.position.x) <= 50f)
        {
            ActivateEnemy();
        }
    }

    public void ActivateEnemy()
    {
        if (movementScript != null)
            movementScript.enabled = true;

        if (enemyShooting != null)
            enemyShooting.enabled = true;

        //if (enemyHealth != null)
        //    enemyHealth.enabled = true;

        Destroy(this);
    }
}
