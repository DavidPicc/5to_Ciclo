using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cores_Script : MonoBehaviour
{
    Rigidbody rb;
    Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        var dir = (player.position - transform.position).normalized;

        rb.velocity = dir * 18f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameScore.instance.AddCores(1);
            Destroy(gameObject);
        }
    }
}
