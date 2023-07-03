using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform target; // The target object to move towards
    public float speed = 1f; // The movement speed

    private bool isMoving = false; // Variable to control if the object should move or not

    public void ActivateMovement()
    {
        isMoving = true; // Activate movement when this method is called
    }

    private void Update()
    {
        if (isMoving)
        {
            // Calculate the direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Calculate the distance to the target
            float distance = Vector3.Distance(transform.position, target.position);

            // If the object has not reached the target, continue moving towards it
            if (distance > 0.1f)
            {
                // Move the object in the direction towards the target
                transform.position += direction * speed * Time.deltaTime;
            }
            else
            {
                isMoving = false; // Disable movement once the target is reached
            }
        }
    }
}
