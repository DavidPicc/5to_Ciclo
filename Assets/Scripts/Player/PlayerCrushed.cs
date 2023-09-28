using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrushed : MonoBehaviour
{
    public GameObject objectInCollider;
    bool done = false;
    public LayerMask crushedLayers;

    public float timer = 0;

    Player_Health pHealth;

    private void Start()
    {
        pHealth = GameObject.FindObjectOfType<Player_Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & crushedLayers) != 0)
        {
            objectInCollider = other.gameObject;
            timer = 0;
            //if (!done)
            //{
            //    if (!done)
            //    {
            //        pHealth.crushed += 1;
            //        done = true;
            //    }

            //    if (pHealth.crushed >= 2)
            //    {
            //        pHealth.Death();
            //    }
            //}
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & crushedLayers) != 0)
        {
            timer += Time.deltaTime;
            if (timer >= 0.2f)
            {
                if (!done)
                {
                    if (!done)
                    {
                        pHealth.crushed += 1;
                        done = true;
                    }

                    if (pHealth.crushed >= 2)
                    {
                        pHealth.Death();
                    }
                }
            }
        }
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
            timer = 0;
            if (done)
            {
                pHealth.crushed -= 1;
                done = false;
            }
        }
    }
}
