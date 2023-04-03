using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_Script : MonoBehaviour
{
    public enum PointState
    {
        scatter,
        go
    }
    public PointState state;
    Rigidbody rb;
    Transform player;
    float currentDrag;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentDrag = rb.drag;
    }

    void CheckForNext()
    {
        state = PointState.go;
    }

    void Update()
    {
        switch(state)
        {
            case PointState.scatter:
                rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, Time.deltaTime * 2);
                Invoke("CheckForNext", 0.8f);
                break;
            case PointState.go:
                //Vector3 goToPlayer = transform.position - player.position;
                //rb.position = Vector3.MoveTowards(transform.position, player.position, 60 * Time.smoothDeltaTime);
                var dir = (player.position - transform.position).normalized;

                rb.velocity = dir * 15f;
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("CHOCOOOO");
            GameScore.instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
