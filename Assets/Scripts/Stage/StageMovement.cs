using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] bool moveWithRB;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(moveWithRB)
        {
            Vector3 movement = Vector3.right * moveSpeed *1000 * Time.deltaTime;
            rb.velocity = movement;
        }
        else
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
}
