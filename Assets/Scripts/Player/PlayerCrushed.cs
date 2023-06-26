using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrushed : MonoBehaviour
{
    public GameObject objectInCollider;
    bool done = false;
    public LayerMask crushedLayers;

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & crushedLayers) != 0)
        {
            objectInCollider = other.gameObject;
            if(!done)
            {
                if (!done)
                {
                    FindObjectOfType<Player_Health>().crushed += 1;
                    done = true;
                }

                if (FindObjectOfType<Player_Health>().crushed >= 2)
                {
                    FindObjectOfType<Player_Health>().Death();
                }
            }
        }
        //if(other.CompareTag("Obstacle") || (((1 << other.gameObject.layer) & crushedLayers) != 0))
        //{
        //    objectInCollider = other.gameObject;
        //    if(!done)
        //    {
        //        FindObjectOfType<Player_Health>().crushed += 1;
        //        done = true;
        //    }

        //    if(FindObjectOfType<Player_Health>().crushed >= 2)
        //    {
        //        FindObjectOfType<Player_Health>().Death();
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Obstacle") || other.gameObject.layer == 7 || other.gameObject.layer == 10)
        //{
        //    objectInCollider = null;
        //    if(done)
        //    {
        //        FindObjectOfType<Player_Health>().crushed -= 1;
        //        done = false;
        //    }
        //}

        if (((1 << other.gameObject.layer) & crushedLayers) != 0)
        {
            objectInCollider = null;
            if (done)
            {
                FindObjectOfType<Player_Health>().crushed -= 1;
                done = false;
            }
        }
    }
}
