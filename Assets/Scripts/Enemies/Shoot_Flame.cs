using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Flame : EnemyShooting
{
    Transform _camera;
    Transform _moveX;

    [Header("General")]
    public GameObject bulletPrefab;
    public float damage;
    public float bulletSpeed;
    public float fireRate;
    public float bulletNumber;
    public float bulletDisappear;
    public float bulletAng;
    public float anticipation;
    [SerializeField] public float maxAngle;
    [SerializeField] public Transform[] shootPoints;
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
        _camera = GameObject.FindGameObjectWithTag("Activator").transform;
        antiSphere.SetActive(false);
        _moveX = FindObjectOfType<StageMovement>().transform;
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
       /* float angle = 0;
        if (bulletNumber > 1)
        {
            angle -= (bulletAng) / 2;
        }
       */
        for (int i = 0; i < bulletNumber; i++)
        {
            float angle;
            angle = Random.Range(-maxAngle, maxAngle);
            Quaternion offsetVector = Quaternion.Euler(0, 0, angle);
            var bullet = Instantiate(bulletPrefab, shootPoints[0].position, shootPoints[0].rotation * offsetVector);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, bulletDisappear);
            audioManager.PlayOneShot(ShootSound);
        }

        shotBullet = true;
    }
}
