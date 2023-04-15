using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * moveSpeed;
    }
}
