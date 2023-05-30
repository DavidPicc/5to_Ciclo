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

    public string[] talkingThings;

    public bool canDie = true;
    public GameObject explosionVFX;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        allyPositions = GameObject.FindGameObjectsWithTag("AllyPositions");

        SetAllyPosition();
        gameObject.transform.parent = FindObjectOfType<StageMovement>().transform;

        canDie = false;
    }

    void SetAllyPosition()
    {
        allyPos = allyPositions[friendPos].transform;
        allyPositions[friendPos].GetComponent<AllyPositionScript>().alreadyOccupied = true;
        allyPositions[friendPos].GetComponent<AllyPositionScript>().occupant = gameObject;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Death();
        }
    }

    //void GoAhead()
    //{
    //    allyLocked = false;
    //    rb.velocity = Vector3.right * maxSpeed * 500 * Time.deltaTime;
    //    GetComponentInChildren<Collider>().enabled = false;
    //}

    public string RandomTalk()
    {
        int random = Random.Range(0,talkingThings.Length);
        return talkingThings[random];
    }

    public void Death()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        allyPos = null;
        rb.useGravity = true;
        Destroy(gameObject, 1f);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyBullet"))
        {
            if(canDie)
            {
                Death();
                Destroy(other.gameObject);
            }
        }
    }
}