using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_Shoot : MonoBehaviour
{
    [SerializeField] int numberOfBullets;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform shootPoint;
    Transform player;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate;
    float timer;

    bool canShoot => !shotBullet && Mathf.Abs(transform.localPosition.x) <= distanceToShoot;
    public float distanceToShoot;
    bool shotBullet = false;

    Transform _camera;
    void Start()
    {
        timer = fireRate;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = FindObjectOfType<StageMovement>().transform;
    }


    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            Shoot();
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

    void Shoot()
    {
        float angle = 0;

        for (int i = 0; i < numberOfBullets; i++)
        {
            Quaternion offsetVector = Quaternion.Euler(0, 0, angle);
            var bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation * offsetVector, _camera);
            bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 5f);
            angle += 360 / numberOfBullets;
        }

        shotBullet = true;
    }
}
