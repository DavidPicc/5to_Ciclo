using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float maxSpeed, minSpeed, distance;
    public GameObject[] allyPositions;
    Transform allyPos;
    public int friendPos;
    bool allyLocked = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        allyPositions = GameObject.FindGameObjectsWithTag("AllyPositions");

        SetAllyPosition();
        gameObject.transform.parent = FindObjectOfType<StageMovement>().transform;
    }

    void SetAllyPosition()
    {
        allyPos = allyPositions[friendPos].transform;
        allyPositions[friendPos].GetComponent<AllyPositionScript>().alreadyOccupied = true;
        allyPositions[friendPos].GetComponent<AllyPositionScript>().occupant = gameObject;
    }
    void Update()
    {
        if(FindObjectOfType<SituationManager>().wave == 7)
        {
            GoAhead();
        }
    }

    void GoAhead()
    {
        allyLocked = false;
        rb.velocity = Vector3.right * maxSpeed * 500 * Time.deltaTime;
        GetComponentInChildren<Collider>().enabled = false;
    }

    void FixedUpdate()
    {
        if (allyPos != null)
        {
            if(allyLocked)
            {
                // calculate the direction and distance to the target
                Vector3 dir = allyPos.position - transform.position;
                float dist = dir.magnitude;

                // calculate the current speed based on distance
                float speed = Mathf.Lerp(minSpeed, maxSpeed, dist / distance);

                // normalize the direction and scale it by the current speed
                dir.Normalize();
                Vector3 moveVector = dir * speed;

                // move towards the target
                rb.velocity = moveVector;
            }
            
        }
    }
}
