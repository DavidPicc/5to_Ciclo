using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Angular : EnemyShooting
{
    Transform _camera;

    [Header("General")]
    public GameObject bulletPrefab;
    public float damage;
    public float bulletSpeed;
    public float fireRate;
    public float bulletNumber;
    public float bulletDisappear;
    public float bulletAng;
    public float anticipation;
    [Header("Shooting")]
    public Transform pointShoot;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShootSound;
    private float shootTimer;
    private bool shotBullet;
    public float distanceToShoot;
    //bool canShoot => !shotBullet && Mathf.Abs(transform.localPosition.x) <= distanceToShoot;
    bool canShoot => !shotBullet && Mathf.Abs(Vector3.Distance(_camera.position, transform.position)) <= distanceToShoot;
    public GameObject antiSphere;
    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        antiSphere.SetActive(false);
    }

    void Update()
    {
        Shoot();
      
    }

    void CheckIfShot()
    {
        if (shootTimer <= 0)
        {
            shotBullet = false;
        }

        if (shotBullet)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            shootTimer = fireRate;
        }
    }

    void Shoot()
    {
        CheckIfShot();
        Anticipation();
        if (canShoot)
        {
            antiSphere.SetActive(false);
            ShootAngular();
        }
    }

    void Anticipation()
    {
        if (!canShoot)
        {
            antiSphere.SetActive(false);
        }
        if (shootTimer <= anticipation)
        {
            //Play ANTICIPATION animation.
            antiSphere.SetActive(true);
        }
    }

    void ShootAngular()
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
            audioManager.PlayOneShot(ShootSound);
        }

        shotBullet = true;
    }
}
