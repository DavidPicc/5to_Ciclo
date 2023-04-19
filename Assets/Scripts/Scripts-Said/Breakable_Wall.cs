using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable_wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
                Destroy(other.gameObject);
        }
    }
}
