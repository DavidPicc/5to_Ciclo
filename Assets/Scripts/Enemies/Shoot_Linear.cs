using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dispara en línea recta.
public class Shoot_Linear : EnemyShooting
{
    Transform _camera;

    [Header("General")]
    public GameObject bulletPrefab;
    public float damage;
    public float bulletSpeed;
    public float fireRate;
    public float bulletNumber;
    public float bulletDisappear;
    public float bulletOffset;
    public float anticipation;
    [Header("Shooting")]
    public Transform pointShoot;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShootSound;
    private float shootTimer;
    private bool shotBullet;
    public float distanceToShoot;
    //bool canShoot => !shotBullet && Mathf.Abs(transform.localPosition.x) <= distanceToShoot;
    bool canShoot => !shotBullet && Vector3.Distance(_camera.position, transform.position) <= distanceToShoot;
    public GameObject antiSphere;
    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        antiSphere.SetActive(false);
    }

    void Update()
    {
        Shoot();
        //Debug.Log(canShoot);
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
            ShootLinear();
        }
    }

    void Anticipation()
    {
        if(!canShoot)
        {
            antiSphere.SetActive(false);
        }
        if (shootTimer <= anticipation)
        {
            //Play ANTICIPATION animation.
            antiSphere.SetActive(true);
        }
    }

    public void ShootLinear()
    {
        float offset = 0;
        if (bulletNumber > 1)
        {
            offset -= (bulletOffset / 10) / 2;
        }

        for (int i = 0; i < bulletNumber; i++)
        {
            Vector3 offsetVector = new Vector3(0, offset, 0);
            var bullet = Instantiate(bulletPrefab, pointShoot.position + offsetVector, Quaternion.identity, _camera);
            bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, bulletDisappear);
            offset += (bulletOffset / 10) / (bulletNumber - 1);
            audioManager.PlayOneShot(ShootSound);
        }

        shotBullet = true;
    }
}
