using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet_Script : MonoBehaviour
{
    public Player_Health playerHealth;
    public Player_Hability playerHability;

    private void Start()
    {
        playerHealth = FindAnyObjectByType<Player_Health>();
        playerHability = FindObjectOfType<Player_Hability>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            playerHealth.TakeDamage(1);
        }
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
            playerHability.TakeDmg++;
        }

        
       
    }
}
