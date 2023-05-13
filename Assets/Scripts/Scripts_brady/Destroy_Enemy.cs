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
    private void OnCollisionEnter (Collision other)

    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet"))
        {
            playerHealth.TakeDamage(1);
            Destroy(other.gameObject);
        }

    if (other.gameObject.CompareTag("s"))

        {
            Destroy(gameObject);
        }
    }
 
}
