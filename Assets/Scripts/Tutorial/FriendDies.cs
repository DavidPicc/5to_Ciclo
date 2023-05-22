using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendDies : MonoBehaviour
{
    Rigidbody rb;
    Transform player;
    public float speed;
    public float distance;
    bool goRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        if(Vector3.Distance(player.position, transform.position) < distance)
        {
            goRight = true;
        }

        if(goRight)
        {
            rb.velocity = Vector3.right * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Obstacle"))
        {
        }
    }
}
