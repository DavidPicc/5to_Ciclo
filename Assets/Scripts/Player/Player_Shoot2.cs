using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot2 : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    public float upgradedShootDamage;
    [SerializeField] bool tribolt;
    public float triboltOpeningAngle;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    float timer;
    bool canShoot => Input.GetKey(KeyCode.Z) && !shotBullet && !GameManager.instance.isPaused && equipped;
    bool shotBullet = false;
    public bool equipped;

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
            bullet.GetComponent<PlayerBullet2>().explosionDamage = shootDamage;
            Destroy(bullet, 8f);

            if (tribolt)
            {
                var bulletUp = Instantiate(bulletPrefab, shootPoints[0].position, Quaternion.identity);
                bulletUp.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0f, 0f, -triboltOpeningAngle/2) * bulletUp.transform.right * bulletSpeed, ForceMode.Impulse);
                bulletUp.GetComponent<PlayerBullet2>().explosionDamage = shootDamage;
                Destroy(bulletUp, 8f);

                var bulletDown = Instantiate(bulletPrefab, shootPoints[0].position, Quaternion.identity);
                bulletDown.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0f, 0f, triboltOpeningAngle/2) * bulletDown.transform.right * bulletSpeed, ForceMode.Impulse);
                bulletDown.GetComponent<PlayerBullet2>().explosionDamage = shootDamage;
                Destroy(bulletDown, 8f);
            }
        }
        shotBullet = true;
    }

    void Shopping()
    {
        tribolt = UpgradeTracker.instance.levels["AreaGun"] > 1;

        if (UpgradeTracker.instance.levels["AreaGun"] > 2)
        {
            shootDamage = upgradedShootDamage;
        }

        equipped = UpgradeTracker.instance.equippedGun == "AreaGun";
    }
}
