using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PushBack : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float repulsionForce = 10f;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] float distanceToPush = 4f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f, obstacleLayer); // Check for nearby colliders in a 2 unit radius on the specified layer

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject) // Exclude self
            {
                Vector3 direction = col.transform.position - transform.position;
                float distance = direction.magnitude;

                if (distance < distanceToPush && Mathf.Abs(col.gameObject.transform.position.x) - Mathf.Abs(transform.position.x) > 0) //If it is on the right side of the object. 
                {
                    // Check for a small distance to avoid division by zero
                    float forceMagnitude = repulsionForce;// / distance; // Force is inversely proportional to distance
                    Vector3 _repulsionForce = -direction.normalized * forceMagnitude;

                    rb.AddForce(_repulsionForce, ForceMode.Force);
                    Debug.Log("funciona");
                }
            }
        }
    }
}
