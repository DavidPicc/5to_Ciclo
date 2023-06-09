using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectActivator : MonoBehaviour
{
    public GameObject objectToMove; // The object to move
    private MoveObject moveObject; // The script of the object to move

    private void Start()
    {
        moveObject = objectToMove.GetComponent<MoveObject>(); // Get the MoveObject component from the object to move
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moveObject.ActivateMovement(); // Call the method in the MoveObject script to activate movement
        }
    }
}
