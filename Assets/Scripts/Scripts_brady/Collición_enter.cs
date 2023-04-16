using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collición_enter : MonoBehaviour
{
   

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            gameObject.SetActive(false);
   
        }

    }

}



