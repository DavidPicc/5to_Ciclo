using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_Script : MonoBehaviour
{
    [SerializeField] float distanceToAutotrack;
    [SerializeField] public float damage;
    [SerializeField] public float dissapearAfterColliding;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            if(other.GetComponentInParent<Enemy_Health>() != null)
            {
                if (other.GetComponentInParent<Enemy_Health>().canBeDamaged)
                {

                    //other.GetComponent<Enemy_Health>().TakeDamage(FindObjectOfType<Player_Shoot>().shootDamage);
                    other.GetComponentInParent<Enemy_Health>().TakeDamage(damage);
                }
                Destroy(gameObject, dissapearAfterColliding);
            }
            
        }
    }
}
