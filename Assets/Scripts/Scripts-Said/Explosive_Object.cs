using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Object : MonoBehaviour
{
    public Player_Health playerHealth;
    public Enemy_Health enemy_Health;
    [SerializeField] public float explosionRadius;

    void Start()
    {
        playerHealth = FindObjectOfType<Player_Health>();
       
    }

    void Update()
    {
        enemy_Health = FindObjectOfType<Enemy_Health>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet")) 
        {         
            Destroy(gameObject);

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player")) 
                {
                    playerHealth.TakeDamage(1);
                    Debug.Log("¡El jugador ha sido impactado por la explosión!");
                }
                if (collider.CompareTag("Enemy"))
                {
                    enemy_Health.TakeDamage(100);
                    Debug.Log("¡Enemigo dentro del radio de la explosión!");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
