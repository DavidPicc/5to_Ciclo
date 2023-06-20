using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot2 : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShootSound;

    float timer;
    bool canShoot => Input.GetKey(KeyCode.Z) && !shotBullet && !GameManager.instance.isPaused && equipped;
    bool shotBullet = false;
    public bool equipped;
    int bulletCount;
    public float inBetweenShotsTime;

    [Header("Upgrade System")]
    [SerializeField] int levelShoot;
    public float upgradedShootDamage;


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
        audioManager = GetComponent<AudioSource>();
        timer = fireRate;
        bulletCount = 1;
        Shopping();
    }


    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            shotBullet = true;
            StartCoroutine(BurstShot());
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
        audioManager.PlayOneShot(ShootSound);
        var bullet = Instantiate(bulletPrefab, shootPoints[0].position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<PlayerBullet2>().explosionDamage = shootDamage;
        Destroy(bullet, 1.3f);
    }

    void Shopping()
    {
        if (UpgradeTracker.instance.levels.ContainsKey("AreaGun"))
        {
            levelShoot = UpgradeTracker.instance.levels["AreaGun"];
        } else UpgradeTracker.instance.levels.Add("AreaGun", levelShoot);

        bulletCount = levelShoot;

        if (levelShoot > 2) shootDamage = upgradedShootDamage;

        equipped = UpgradeTracker.instance.equippedGun == "AreaGun";
    }
}
