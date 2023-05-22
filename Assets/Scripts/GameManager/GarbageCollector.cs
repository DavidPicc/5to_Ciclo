using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Friend"))
        {
            return;
        }
        Destroy(other.gameObject);
        //if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet") || other.CompareTag("Obstacle") || other.CompareTag("chains"))
        //{
        //    Destroy(other.gameObject);
        //}
    }
}
