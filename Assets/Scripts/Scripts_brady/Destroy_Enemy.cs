using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Enemy : MonoBehaviour
{
    public Player_Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter (Collider other)

    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            playerHealth.TakeDamage(1);
            Destroy(other.gameObject);
        }

    if (other.CompareTag("s"))

        {
            Destroy(gameObject);
        }
    }
 
}
