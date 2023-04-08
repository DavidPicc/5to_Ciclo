using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_Movement : MonoBehaviour
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
        GetComponent<Enemy3_Shoot>().shootPoint.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }
}
