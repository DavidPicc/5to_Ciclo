using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2NEW_Shoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
    private float shootInterval = 1f; 
    private float timeSinceLastShot = 0f; 

    void Update()
    {
        timeSinceLastShot += Time.deltaTime; 

        if (timeSinceLastShot >= shootInterval) 
        {
            Shoot(); 
            timeSinceLastShot = 0f; 
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = new Vector2(-3f, 0f); 
    }
}
