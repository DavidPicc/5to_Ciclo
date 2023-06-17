using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet_Script : MonoBehaviour
{
    public Player_Health playerHealth;
    public Player_Ability playerAbility;
   
   
    private void Start()
    {
        playerHealth = FindAnyObjectByType<Player_Health>();
        playerAbility = FindObjectOfType<Player_Ability>();
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("destructible"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            playerHealth.TakeDamage(1);
            Destroy(gameObject);
        }
        //if (other.CompareTag("Shield"))
        //{
        //    Destroy(gameObject);
        //    playerAbility.takeDmg++;
        //}

    }
    
}
