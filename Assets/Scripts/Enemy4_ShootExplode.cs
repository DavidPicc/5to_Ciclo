using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4_ShootExplode : MonoBehaviour
{
    Transform _camera;
    Transform player;

    [Header("General")]
    public GameObject bulletPrefab;
    public float damage;
    public float bulletSpeed;
    public float bulletNumber;
    public float bulletDisappear;
    public float bulletAng;
    public float anticipation;
    [Header("Exploding")]
    public Transform pointShoot;
    private float explodeTimer;
    private bool shotBullet;
    public float distanceToExplode;
    //bool canShoot => !shotBullet && Mathf.Abs(transform.localPosition.x) <= distanceToShoot;
    bool canShoot => !shotBullet;
    bool willExplode = false;
    public GameObject antiSphere;
    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        explodeTimer = anticipation;
        antiSphere.SetActive(false);
    }

    void Update()
    {
        CloseToPlayer();
        if(willExplode)
        {
            Shoot();
        }
    }

    //void CheckIfShot()
    //{
    //    if (shootTimer <= 0)
    //    {
    //        shotBullet = false;
    //    }

    //    if (shotBullet)
    //    {
    //        shootTimer -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        shootTimer = fireRate;
    //    }
    //}

    void Shoot()
    {
        //CheckIfShot();
        Anticipation();
        if (explodeTimer <= 0)
        {
            antiSphere.SetActive(false);
            ShootAngular();
            Destroy(gameObject);
        }
        //if (canShoot)
        //{
        //    antiSphere.SetActive(false);
        //    ShootAngular();
        //}
    }

    void CloseToPlayer()
    {
        if(Vector3.Distance(player.position, transform.position) <= distanceToExplode)
        {
            willExplode = true;
        }
    }

    void Anticipation()
    {
        explodeTimer -= Time.deltaTime;
        if(explodeTimer < anticipation)
        {
            antiSphere.SetActive(true);
        }
        //if (shootTimer <= anticipation)
        //{
        //    //Play ANTICIPATION animation.
        //    antiSphere.SetActive(true);
        //}
    }

    public void ShootAngular()
    {
        float angle = 0;
        if (bulletNumber > 1)
        {
            angle -= (bulletAng) / 2;
        }

        for (int i = 0; i < bulletNumber; i++)
        {
            Quaternion offsetVector = Quaternion.Euler(0, 0, angle);
            var bullet = Instantiate(bulletPrefab, pointShoot.position, pointShoot.rotation * offsetVector, _camera);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, bulletDisappear);
            angle += bulletAng / (bulletNumber);
        }

        shotBullet = true;
    }
}
