using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    public float speedLimit;
    public float moveX, moveY;
    public float moveDrag, stayDrag;
    public Vector3 moveVector;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveVector = new Vector3(moveX, moveY, 0).normalized;
    }

    void FixedUpdate()
    {
        Movement();
        Drag();
    }

    public void Movement()
    {
        if(Mathf.Abs(rb.velocity.x) < speedLimit)
            rb.AddForce(moveVector.x * Vector3.right * moveSpeed, ForceMode.Acceleration);
        if (Mathf.Abs(rb.velocity.y) < speedLimit)
            rb.AddForce(moveVector.y * Vector3.up * moveSpeed, ForceMode.Acceleration);
    }

    public void Drag()
    {
        if(moveX != 0 || moveY != 0)
        {
            rb.drag = moveDrag;
        }
        else
        {
            rb.drag = stayDrag;
        }
    }
}
