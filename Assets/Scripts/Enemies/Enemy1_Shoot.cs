using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Shoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
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
        var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, _camera);
        bullet.GetComponent<Rigidbody>().AddForce(transform.right * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 5f);

        shotBullet = true;
    }

    void Shoot_Exploding()
    {
        var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, _camera);
        bullet.GetComponent<Rigidbody>().AddForce(transform.right * bulletSpeed, ForceMode.Impulse);

        shotBullet = true;
    }
}
