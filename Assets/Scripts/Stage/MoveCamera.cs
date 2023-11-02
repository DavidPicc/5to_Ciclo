using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Transform player;
    StageMovement_Vertical moveVertical;
    Transform moveHorizontal;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        moveVertical = FindObjectOfType<StageMovement_Vertical>();
        moveHorizontal = FindObjectOfType<StageMovement>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!moveVertical.freeCamera)
            {
                player.parent = moveHorizontal;
                moveVertical.freeCamera = true;
                moveVertical.startMovement = true;
            }
            else
            {
                player.parent = moveVertical.transform;
                moveVertical.freeCamera = false;
            }
            //if(startMoving)
            //{
            //    player.parent = moveHorizontal;
            //    moveVertical.freeCamera = true;
            //    moveVertical.startMovement = true;
            //}
            //else
            //{
            //    player.parent = moveVertical.transform;
            //    moveVertical.freeCamera = false;
            //}
            Destroy(gameObject);
        }
    }
}
