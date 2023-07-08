using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot2 : ShopObject
{
    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject Gun;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShootSound;

    float timer;
    bool canShoot => Input.GetKey(KeyCode.Z) && !shotBullet && !GameManager.instance.isPaused && equipped;
    bool shotBullet = false;
    int bulletCount;
    public float inBetweenShotsTime;

    [Header("Upgrade System")]
    public float upgradedShootDamage;

    protected override void Start()
    {
        base.Start();

        //audioManager = GetComponent<AudioSource>();
        timer = fireRate;
        bulletCount = 1;
    }


    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            shotBullet = true;
            StartCoroutine(BurstShot());
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

    IEnumerator BurstShot()
    {
        for(int i = 0; i < bulletCount; i++)
        {
            Shoot_Normal();

            yield return new WaitForSeconds(inBetweenShotsTime);
        }

        yield return null;
    }

    void Shoot_Normal()
    {
        AudioManager.instance.PlaySFX(audioManager, ShootSound, 0.5f);
        var bullet = Instantiate(bulletPrefab, shootPoints[0].position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<PlayerBullet2>().explosionDamage = shootDamage;
        Destroy(bullet, 1.3f);
    }

    protected override void Shopping()
    {
        base.Shopping();

        bulletCount = level;

        if (level > 2) shootDamage = upgradedShootDamage;
    }
}
