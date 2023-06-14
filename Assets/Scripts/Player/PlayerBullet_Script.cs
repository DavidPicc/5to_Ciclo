using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_Script : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float dissapearAfterColliding;
   
        public void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            if(other.GetComponent<Enemy_Health>() != null)
            {
                if (other.GetComponent<Enemy_Health>().canBeDamaged && !other.GetComponent<Enemy_Health>().invulnerable)
                {

                    //other.GetComponent<Enemy_Health>().TakeDamage(FindObjectOfType<Player_Shoot>().shootDamage);
                    other.GetComponent<Enemy_Health>().TakeDamage(damage);
                }
                Destroy(gameObject, dissapearAfterColliding);
            }
        }
        if (other.CompareTag("EnemyBullet"))
        {
            if(other.GetComponent<Enemy8_Bullet>() != null)
            {
                if (other.GetComponent<Enemy8_Bullet>().canBeDamaged)
                {
                    other.GetComponent<Enemy8_Bullet>().TakeDamage(1);
                }
            }
            
        }
    }
}
