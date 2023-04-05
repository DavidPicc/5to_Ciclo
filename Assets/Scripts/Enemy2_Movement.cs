using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_Movement : MonoBehaviour
{
    public enum EnemyStates
    {
        going,
        shooting,
        leaving
    }
    Rigidbody rb;
    public EnemyStates states;
    public GameObject[] hoverPoints;
    public Transform hoverPosition;
    [SerializeField] float moveSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hoverPoints = GameObject.FindGameObjectsWithTag("HoverPoint");
        Invoke("InvokeStart", 0.2f);
    }

    void InvokeStart()
    {
        for (int i = 0; i < hoverPoints.Length; i++)
        {
            if (!hoverPoints[i].gameObject.GetComponent<HoverPoint>().occupied)
            {
                hoverPosition = hoverPoints[i].transform;
                hoverPoints[i].gameObject.GetComponent<HoverPoint>().occupied = true;
            }
        }
    }

    void Update()
    {
        switch(states)
        {
            case EnemyStates.going:
                if(hoverPosition != null)
                {
                    //rb.position = Vector3.MoveTowards(rb.position, hoverPosition.position, moveSpeed * 100 * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, hoverPosition.position, moveSpeed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, hoverPosition.position) <= 0.05f)
                    {
                        states = EnemyStates.shooting;
                    }
                }
                
                
                break;
            case EnemyStates.shooting:
                GetComponent<Enemy2_Shoot>().Shoot();
                break;
            case EnemyStates.leaving:
                transform.position += Vector3.right * moveSpeed;
                break;
        }
    }
}
