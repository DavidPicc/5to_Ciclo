using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot3 : ShopObject
{
    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject Gun;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    [SerializeField] public float maxAngle;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShootSound;

    float timer;
    bool canShoot => Input.GetKey(KeyCode.Z) && !shotBullet && !GameManager.instance.isPaused && equipped;
    bool shotBullet = false;

    [Header("Upgrades System")]
    public float upgradedShootDamage;
    public float upgradedBulletSpeed;
    public float upgradedFireRate;

    protected override void Start()
    {
        base.Start();

        //audioManager = GetComponent<AudioSource>();
        timer = fireRate;
    }


    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            Shoot_Angle();
        }

        if (equipped == true)
        {
            Gun.SetActive(true);
        }
        if (equipped == false)
        {
            Gun.SetActive(false);
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
        AudioManager.instance.PlaySFX(audioManager, ShootSound, 0.5f);
        var bullet = Instantiate(bulletPrefab, shootPoints[0].position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 8f);
        shotBullet = true;
    }

    void Shoot_Angle()
    {
        float angle = 0;
        angle = Random.Range(-maxAngle, maxAngle);
        Quaternion offsetVector = Quaternion.Euler(0, 0, angle);
        var bullet = Instantiate(bulletPrefab, shootPoints[0].position, shootPoints[0].rotation * offsetVector);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 0.7f);
        shotBullet = true;
    }

    protected override void Shopping()
    {
        base.Shopping();

        if (level > 1){
            bulletSpeed = upgradedBulletSpeed;
            fireRate = upgradedFireRate;
        }

        if (level > 2) shootDamage = upgradedShootDamage;
    }
}
