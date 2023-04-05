using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5_Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = transform.right * moveSpeed;
        GetComponent<Enemy5_Shoot>().shootPoint.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }
}
