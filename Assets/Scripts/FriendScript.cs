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
                Vector3 dir = allyPos.position - transform.position;
                float dist = dir.magnitude;

                float speed = Mathf.Lerp(minSpeed, maxSpeed, dist / distance);

                dir.Normalize();
                Vector3 moveVector = dir * speed;

                rb.velocity = moveVector;
            }
        }
    }
}
