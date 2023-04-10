using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2NEW_Movement : MonoBehaviour
{
    private bool moveRight = true; 
    [SerializeField] float moveSpeed;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (moveRight) 
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if (Time.timeSinceLevelLoad >= 3f)
            {
                moveRight = false; 
            }
        }
        else 
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
