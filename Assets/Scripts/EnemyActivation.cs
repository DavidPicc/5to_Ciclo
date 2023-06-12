using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    GameObject player;

    public EnemyMovement movementScript;
    public EnemyShooting enemyShooting;

    public float checkTimer = 2f;
    float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (movementScript != null)
            movementScript.enabled = false;

        if (enemyShooting != null)
            enemyShooting.enabled = false;

        timer = 0;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if(Vector3.Distance(player.transform.position, transform.position) <= 40f)
            {
                ActivateEnemy();
            }
            timer = checkTimer;
        }
    }

    public void ActivateEnemy()
    {
        if (movementScript != null)
            movementScript.enabled = true;

        if (enemyShooting != null)
            enemyShooting.enabled = true;

        Destroy(this);
    }
}
