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
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _camera = GameObject.FindObjectOfType<StageMovement_Vertical>().transform;

        if (movementScript != null)
            movementScript.enabled = false;

        if (enemyShooting != null)
            enemyShooting.enabled = false;

        if (enemyHealth != null)
            enemyHealth.enabled = false;

        //if (Mathf.Abs(transform.position.y) - Mathf.Abs(_camera.transform.position.y) > 30f && Mathf.Abs(transform.position.x) - Mathf.Abs(_camera.transform.position.x) > 55f)
        //{
        // Destroy(gameObject);
        //}


        if (Vector3.Distance(transform.position, player.transform.position) <= 6f)
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
        if (NearPlayer())
        {
            ActivateEnemy();
        }
        else
        {
            if(enemyHealth.enabled)
            {
                Destroy(gameObject);
            }
            
        }
    }

    public void ActivateEnemy()
    {
        if (movementScript != null)
            movementScript.enabled = true;

        if (enemyShooting != null)
            enemyShooting.enabled = true;

        if (enemyHealth != null)
            enemyHealth.enabled = true;

        //Destroy(this);
    }

    public bool NearPlayer()
    {
        return (Mathf.Abs(transform.position.y - _camera.transform.position.y) <= 30f && Mathf.Abs(transform.position.x - _camera.transform.position.x) <= 45f);
    }

    public void ShowDistance()
    {
        float yDistance = Mathf.Abs(transform.position.y) - Mathf.Abs(_camera.transform.position.y);
        float xDistance = Mathf.Abs(transform.position.x) - Mathf.Abs(_camera.transform.position.x);
        Debug.Log(name + ": Distancia en X es " + xDistance.ToString());
        Debug.Log(name + ": Distancia en Y es " + yDistance.ToString());
    }
}
