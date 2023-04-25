using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_Script : MonoBehaviour
{
    [SerializeField] float distanceToAutotrack;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy_Health>().canBeDamaged)
            {
                other.GetComponent<Enemy_Health>().TakeDamage(FindObjectOfType<Player_Shoot>().shootDamage);

            }
            Destroy(gameObject);
        }
    }
}
