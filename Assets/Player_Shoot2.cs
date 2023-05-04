using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot2 : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    float timer;
    bool canShoot => Input.GetMouseButton(0) && !shotBullet && !GameManager.instance.isPaused;
    bool shotBullet = false;
    void Start()
    {
        timer = fireRate;
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
        for (int i = 0; i < GetComponent<Player_Manager>().maxCannons; i++)
        {
            var bullet = Instantiate(bulletPrefab, shootPoints[0].position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 8f);
        }
        shotBullet = true;
    }
}