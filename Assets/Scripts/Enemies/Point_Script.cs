using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Point_Script : MonoBehaviour
{
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip PointsSound;

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
        audioManager = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentDrag = rb.drag;

        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector3 randomVector = new Vector3(randomX, randomY, 0);
        rb.AddForce(randomVector * 10f, ForceMode.Impulse);
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
                rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, Time.deltaTime * 5);
                Invoke("CheckForNext", 0.5f);
                break;
            case PointState.go:
                //Vector3 goToPlayer = transform.position - player.position;
                //rb.position = Vector3.MoveTowards(transform.position, player.position, 60 * Time.smoothDeltaTime);
                var dir = (player.position - transform.position).normalized;

                rb.velocity = dir * 18f;
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioManager.PlayOneShot(PointsSound);
            GameScore.instance.AddGears(1);
            Destroy(gameObject);
        }

    }
}
