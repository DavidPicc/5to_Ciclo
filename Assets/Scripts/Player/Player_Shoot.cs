using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    [HideInInspector] public int gunLevel = 0;
    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    public float upgradedShootDamage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    [SerializeField] GameObject vfxShoot;
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
        Upgrades();
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
        for(int i = 0; i < GetComponent<Player_Manager>().maxCannons; i++)
        {
            var bullet = Instantiate(bulletPrefab, shootPoints[i].position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            bullet.GetComponent<PlayerBullet_Script>().damage = shootDamage;
            
            GameObject vfx = Instantiate(vfxShoot, shootPoints[i].position, Quaternion.Euler(0f, 90f, 90f));
            //vfx.transform.SetParent(bullet.transform, false);

            Destroy(bullet, 2f);
        }
        shotBullet = true;
    }

    public void Upgrades()
    {
        switch(gunLevel)
        {
            case 0:
                fireRate = 0.15f;
                break;
            case 1:
                fireRate = 0.08f;
                break;
            case 2:
                fireRate = 0.05f;
                break;
            case 3:
                fireRate = 0.03f;
                break;
        }
    }

    void Shopping()
    {
        int levelShoot = UpgradeTracker.instance.levels["StraightGun"];

        if (levelShoot > 2) shootDamage = upgradedShootDamage;

        equipped = UpgradeTracker.instance.equippedGun == "StraightGun";
    }
}
