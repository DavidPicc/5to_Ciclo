using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ability2 : ShopObject
{
    [SerializeField] float rechargeBar;
    [SerializeField] float maxRechargeBar;
    [SerializeField] Image abilityBar;
    [SerializeField] int maxBullets = 3;
    [SerializeField] Transform pivot;
    [SerializeField] float shieldDistance;
    [SerializeField] float rotationSpeed = 90f;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShieldSound;
    List<GameObject> bullets = new List<GameObject>();
    public bool endShield;
    public bool activate;
    public float timer;
    public float maxtimer;

    public GameObject bulletPrefab;

    [Header("Upgrade System")]
    public int upgradedMaxBullets;

    protected override void Start()
    {
        base.Start();

        //audioManager = GetComponent<AudioSource>();
        rechargeBar = maxRechargeBar;
    }

    void Update()
    {
        if (!GameManager.instance.isPaused && GameManager.instance.canUseAbilities && Input.GetKey(KeyCode.X) && rechargeBar >= maxRechargeBar && !activate && equipped)
        {
            SpawnBulletShield();
            activate = true;
        }

        if(activate)
        {
            rechargeBar -= Time.deltaTime;
            if(equipped) abilityBar.fillAmount = rechargeBar/maxRechargeBar;
            AudioManager.instance.PlaySFX(audioManager, ShieldSound, 0.5f);
            if (rechargeBar <= 0)
            {
                PushBullets();
                activate = false;
            }
        }
        else
        {
            rechargeBar += Time.deltaTime;
            rechargeBar = Mathf.Clamp(rechargeBar, 0f, maxRechargeBar);
            if(equipped) abilityBar.fillAmount = rechargeBar / maxRechargeBar;
        }
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i] != null)
            {
                bullets[i].transform.RotateAround(pivot.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                Destroy(bullets[i], rechargeBar);
            }
        }
    }

    void SpawnBulletShield()
    {
        float angleStep = 360 / (maxBullets - 0);
        float angle = -180 / 2;
        for (int i = 0; i < maxBullets; i++)
        {
            float x = shieldDistance * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = shieldDistance * Mathf.Sin(Mathf.Deg2Rad * angle);
            Vector3 spawnPosition = pivot.position + new Vector3(x, y, 0f);
            Quaternion spawnRotation = Quaternion.Euler(0, 0, angle + 90);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            bullet.transform.parent = pivot;
            bullets.Add(bullet);
            angle += angleStep;
        }
    }

    void PushBullets()
    {
        for(int i = 0; i <bullets.Count; i++)
        {
            if (bullets[i] != null)
            {
                bullets[i].GetComponent<Rigidbody>().AddForce(-bullets[i].transform.up * 4f, ForceMode.Impulse);
               
            }
        }
    }

    protected override void Shopping()
    {
        base.Shopping();

        if (level > 1)
        {
            maxBullets = upgradedMaxBullets;
        }
    }
}
