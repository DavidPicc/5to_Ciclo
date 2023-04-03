using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    Transform player;
    public float shootSpeed;
    public float fireRate;
    float timer;

    bool canShoot => !shotBullet && transform.position.x - player.transform.position.x <= distanceToShoot;
    public float distanceToShoot;
    bool shotBullet = false;
    void Start()
    {
        timer = fireRate;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            Shoot_Normal();
        }
    }

    void CheckIfShot()
    {
        if (timer <= 0)
        {
            shotBullet = false;
        }

        if (shotBullet)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = fireRate;
        }

    }

    void Shoot_Normal()
    {
        var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, transform);
        bullet.GetComponent<Rigidbody>().AddForce(transform.right * shootSpeed, ForceMode.Impulse);
        Destroy(bullet, 5f);

        shotBullet = true;
    }
}
