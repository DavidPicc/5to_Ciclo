using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectActivator : MonoBehaviour
{
    public GameObject objectToMove; 
    private MoveObject moveObject; 

    private void Start()
    {
        moveObject = objectToMove.GetComponent<MoveObject>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moveObject.ActivateMovement();
        }
    }
}
