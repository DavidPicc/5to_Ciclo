using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_Shoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
    Transform player;
    [SerializeField] float prepRay, rayTime;
    float timer;

    public float distanceToShoot;
    bool isShooting = false;
    void Start()
    {
        timer = prepRay;
    }

    void Update()
    {
        
    }



    public void Shoot()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if(isShooting)
            {
                GetComponent<Enemy2_Movement>().states = Enemy2_Movement.EnemyStates.leaving;
            }
            var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, transform);
            isShooting = true;
            timer = rayTime;
        }
        
    }
}
