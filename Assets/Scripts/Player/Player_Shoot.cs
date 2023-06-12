using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    [HideInInspector] public int gunLevel = 0;
    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    [SerializeField] float bulletsOffset;
    //[SerializeField] GameObject vfxShoot;
    public int cannons;
    public int cannon;
    float timer;
    bool canShoot => Input.GetKey(KeyCode.Z) && !shotBullet && !GameManager.instance.isPaused && equipped;
    bool shotBullet = false;
    public bool equipped;

    [Header("Upgrade System")]
    [SerializeField] int levelShoot;
    public float upgradedShootDamage;
    public int upgradeCannons;
    public int upgradedCannon;

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
     //   if (vfxShoot != null)
     //   {
     //       var vfxShootp = Instantiate(vfxShoot, transform.position, Quaternion.identity);
      //      vfxShootp.transform.forward = gameObject.transform.forward;
     //   }
        timer = fireRate;
        levelShoot = 1;
        Shopping();
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
        float randomOffset = Random.Range(-bulletsOffset, bulletsOffset)/10f;
        Vector3 vecOffset = new Vector3(0, randomOffset, 0);
        for(int i = cannon; i < cannons; i++)
        {
            var bullet = Instantiate(bulletPrefab, shootPoints[i].position + vecOffset, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            bullet.GetComponent<PlayerBullet_Script>().damage = shootDamage;
           
         //   GameObject vfx = Instantiate(vfxShoot, shootPoints[i].position, Quaternion.Euler(0f, 90f, 90f));
            //vfx.transform.SetParent(bullet.transform, false);

            Destroy(bullet, 2f);
        }
        shotBullet = true;
    }

    void Shopping()
    {
        if (UpgradeTracker.instance.levels.ContainsKey("StraightGun"))
        {
            levelShoot = UpgradeTracker.instance.levels["StraightGun"];
        }

        if (levelShoot > 2) shootDamage = upgradedShootDamage;

        if (levelShoot > 1) cannons = upgradeCannons;

        equipped = UpgradeTracker.instance.equippedGun == "StraightGun";
    }
}
