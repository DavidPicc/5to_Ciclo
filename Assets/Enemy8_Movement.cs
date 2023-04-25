using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8_Movement : MonoBehaviour
{
    public GameObject randomPoint;
    public float maxDistance;
    public LayerMask cameraBorderLayer;
    Rigidbody rb;
    public float moveSpeed;
    public float timeToMove;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = timeToMove;
        StartCoroutine(MoveEnemy());
    }

    void Update()
    {
        //StartCoroutine(MoveEnemy());
        //timer -= Time.deltaTime;
        //if(timer <= 0)
        //{
        //    StartCoroutine(MoveEnemy());
        //    timer = timeToMove;
        //}
    }

    private IEnumerator MoveEnemy()
    {
        while (true)
        {
            // Generate a random direction and move position within the move radius
            //Vector3 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 randomDirection = new Vector3(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance), 0);
            Vector3 movePosition = transform.position + randomDirection;

            // Check if the move position overlaps with a collider on the "CameraBorder" layer
            Collider[] colliders = Physics.OverlapSphere(movePosition, 0.1f, cameraBorderLayer);
            if (colliders.Length == 0)
            {
                while (Vector3.Distance(transform.position, movePosition) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, movePosition, moveSpeed * Time.deltaTime);
                    yield return null;
                }
                transform.position = movePosition; // Set the final position to avoid potential drift
                yield return new WaitForSeconds(timeToMove);
            }
            else
            {
                // Wait for a short time before trying again
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    //void Move()
    //{
    //    float randomX = Random.Range(-maxDistance, maxDistance);
    //    float randomY = Random.Range(-maxDistance, maxDistance);
    //    randomPoint.transform.position += new Vector3(randomX, randomY, 0);

    //    Collider[] colliders = Physics.OverlapSphere(randomPoint.transform.position, randomPoint.GetComponent<SphereCollider>().radius, LayerMask.NameToLayer("CameraBorders"));
    //    foreach (Collider collider in colliders)
    //    {
    //        // Ignore self-collision
    //        if (collider == randomPoint.GetComponent<SphereCollider>())
    //            continue;

    //        // Check if the other collider is a sphere collider
    //        Collider otherSphereCollider = collider.GetComponent<Collider>();
    //        if (otherSphereCollider.gameObject.layer == 7)
    //        {
    //            Debug.Log("Collision detected!");
    //            // Do something when collision is detected
    //        }
    //    }

    //    Transform goToPoint = randomPoint.transform;
    //    rb.position = Vector3.MoveTowards(transform.position, goToPoint.position, moveSpeed * Time.deltaTime);

    //}
}
