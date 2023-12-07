using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_Script : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float dissapearAfterColliding;
    public GameObject explosionPrefab;
    public bool Lanzallamas;

    public void Start()
    {
        Destroy(gameObject, 20f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle")|| other.CompareTag("destructible"))
        {
            if(Lanzallamas == false)
            {
                GameObject impact = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(impact, 1f);
            }
           
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
            }
            if(Lanzallamas == false)
            {
                GameObject impact = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(impact, 1f);
                Destroy(gameObject);
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
