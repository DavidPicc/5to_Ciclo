using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Player_Shoot : ShopObject
{
    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject Gun;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    [SerializeField] float bulletsOffset;
    //[SerializeField] GameObject vfxShoot;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShootSound;

    public int cannons;
    public int cannon;
    float timer;
    bool canShoot => Input.GetKey(KeyCode.Z) && !shotBullet && !GameManager.instance.isPaused && equipped;
    bool shotBullet = false;

    [Header("Upgrade System")]
    public float upgradedShootDamage;
    public int upgradeCannons;
    public int upgradedCannon;

    protected override void Start()
    {
        base.Start();

        //   if (vfxShoot != null)
        //   {
        //       var vfxShootp = Instantiate(vfxShoot, transform.position, Quaternion.identity);
        //      vfxShootp.transform.forward = gameObject.transform.forward;
        //   }
        //audioManager = GetComponent<AudioSource>();
        timer = fireRate;
    }


    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            Shoot_Normal();
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
        float randomOffset = Random.Range(-bulletsOffset, bulletsOffset)/10f;
        Vector3 vecOffset = new Vector3(0, randomOffset, 0);
        for(int i = cannon; i < cannons; i++)
        {
            var bullet = Instantiate(bulletPrefab, shootPoints[i].position + vecOffset, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            bullet.GetComponent<PlayerBullet_Script>().damage = shootDamage;

            //   GameObject vfx = Instantiate(vfxShoot, shootPoints[i].position, Quaternion.Euler(0f, 90f, 90f));
            //vfx.transform.SetParent(bullet.transform, false);
            AudioManager.instance.PlaySFX(audioManager, ShootSound, 0.5f);
            Destroy(bullet, 2f);
        }
        shotBullet = true;
    }

    protected override void Shopping()
    {
        base.Shopping();

        if (level > 2) shootDamage = upgradedShootDamage;

        if (level > 1) cannons = upgradeCannons;
    }
}
