using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    GameObject player;
    [SerializeField] Transform _camera;

    public EnemyMovement movementScript;
    public EnemyShooting enemyShooting;
    public Enemy_Health enemyHealth;

    bool activated = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(FindObjectOfType<StageMovement_Vertical>() != null)
        {
            _camera = FindObjectOfType<StageMovement_Vertical>().transform;
        }
        else
        {
            _camera = FindObjectOfType<StageMovement>().transform;
        }

        if(GetComponent<Enemy_Health>() != null)
        {
            enemyHealth = GetComponent<Enemy_Health>();
        }

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


        if (Vector3.Distance(transform.position, player.transform.position) <= 8f)
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
        if (!activated && NearPlayer())
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

        if (enemyHealth != null)
            enemyHealth.enabled = true;

        activated = true;
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
