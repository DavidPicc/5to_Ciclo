using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot3 : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    [SerializeField] public float maxAngle;
    float timer;
    bool canShoot => Input.GetKey(KeyCode.Z) && !shotBullet && !GameManager.instance.isPaused && equipped;
    bool shotBullet = false;
    public bool equipped;

    [Header("Upgrades System")]
    [SerializeField] int levelShoot;
    public float upgradedShootDamage;
    public float upgradedBulletSpeed;
    public float upgradedFireRate;

    private void OnEnable()
    {
        GameManager.onShopApply += Shopping;
    }

    private void OnDisable()
    {
        GameManager.onShopApply -= Shopping;
    }

    void Start()
    {
        timer = fireRate;
        levelShoot = 0;
    }


    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            Shoot_Angle();
            //Shoot_Normal();
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

    void Shoot_Angle()
    {
        float angle = 0;
        angle = Random.Range(-maxAngle, maxAngle);
        for (int i = 0; i < GetComponent<Player_Manager>().maxCannons; i++)
        {
            Quaternion offsetVector = Quaternion.Euler(0, 0, angle);
            var bullet = Instantiate(bulletPrefab, shootPoints[0].position, shootPoints[0].rotation * offsetVector);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 0.7f);
        }
        shotBullet = true;
    }

    void Shopping()
    {
        
        if (UpgradeTracker.instance.levels.ContainsKey("Flamethrower"))
        {
            levelShoot = UpgradeTracker.instance.levels["Flamethrower"];
        }

        if (levelShoot > 1){
            bulletSpeed = upgradedBulletSpeed;
            fireRate = upgradedFireRate;
        }

        if (levelShoot > 2) shootDamage = upgradedShootDamage;

        equipped = UpgradeTracker.instance.equippedGun == "Flamethrower";
    }
}
