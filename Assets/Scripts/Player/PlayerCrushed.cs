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

    private void Update()
    {
        if(objectInCollider == null && done)
        {
            ExitCollider();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & crushedLayers) != 0)
        {
            objectInCollider = other.gameObject;
            timer = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & crushedLayers) != 0)
        {
            if (!done)
            {
                timer += Time.deltaTime;
                if (timer >= 0.2f)
                {
                    pHealth.crushed += 1;

                    if (pHealth.crushed >= 2)
                    {
                        pHealth.Death();
                    }

                    done = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ExitCollider();
    }

    void ExitCollider()
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
