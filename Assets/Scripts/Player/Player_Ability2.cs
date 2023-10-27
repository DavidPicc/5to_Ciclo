using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ability2 : MonoBehaviour
{
    public bool equipped;

    [Header("Shield Charge")]
    [SerializeField] float rechargeBar;
    public float maxRechargeBar;
    [SerializeField] Image abilityBar;
    public int maxBullets = 3;
    public float damage;

    [Header("References")]
    public GameObject Shield;
    public GameObject EscudoSierrasUI;
    [SerializeField] Transform pivot;
    [SerializeField] float shieldDistance;
    [SerializeField] float rotationSpeed = 90f;
    [SerializeField] public AudioSource audioManager;
    bool abilityChargedSoundPlayed = false;
    [SerializeField] public AudioClip AbilityChargedSound;
    [SerializeField] public AudioClip ShieldSound;
    List<GameObject> bullets = new List<GameObject>();
    public bool endShield;
    public bool activate;
    public float timer;
    public float maxtimer;

    public GameObject bulletPrefab;

    void Start()
    {
        //audioManager = GetComponent<AudioSource>();
        rechargeBar = maxRechargeBar;
    }

    void Update()
    {
        if (!GameManager.instance.isPaused && GameManager.instance.canUseAbilities && Input.GetKey(KeyCode.X) && rechargeBar >= maxRechargeBar && !activate && equipped)
        {
            SpawnBulletShield();
            PlayShieldSound();
            activate = true;
            abilityChargedSoundPlayed = false;
            if (Shield != null) Shield.SetActive(true);
        }

        if(activate)
        {
            rechargeBar -= Time.deltaTime;
            if(equipped) abilityBar.fillAmount = rechargeBar/maxRechargeBar;
            //AudioManager.instance.PlaySFX(audioManager, ShieldSound, 0.5f);
            if (rechargeBar <= 0)
            {
                PushBullets();
                StopShieldSound();
                activate = false;
                if (Shield != null) Shield.SetActive(false);
            }
        }
        else
        {
            rechargeBar += Time.deltaTime;
            rechargeBar = Mathf.Clamp(rechargeBar, 0f, maxRechargeBar);
            if(equipped) abilityBar.fillAmount = rechargeBar / maxRechargeBar;
            if (rechargeBar >= maxRechargeBar && !abilityChargedSoundPlayed)
            {
                AudioManager.instance.PlaySFX(audioManager, AbilityChargedSound, 1.0f);
                abilityChargedSoundPlayed = true;
            }
        }
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i] != null)
            {
                bullets[i].transform.RotateAround(pivot.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                Destroy(bullets[i], rechargeBar);
            }
        }

        if (equipped == true)
        {
            EscudoSierrasUI.SetActive(true);
        }
        if (equipped == false)
        {
            EscudoSierrasUI.SetActive(false);
        }
    }

    void PlayShieldSound()
    {
        audioManager.volume = 1;
        audioManager.clip = ShieldSound;
        audioManager.loop = true;
        audioManager.Play();
    }

    void StopShieldSound()
    {
        audioManager.loop = false;
        audioManager.Stop();
    }

    void SpawnBulletShield()
    {
        float angleStep = 360f / maxBullets; 
        float angle = -180f / 2f; 

        for (int i = 0; i < maxBullets; i++)
        {
            float x = shieldDistance * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = shieldDistance * Mathf.Sin(Mathf.Deg2Rad * angle);
            Vector3 spawnPosition = pivot.position + new Vector3(x, y, 0f);
            Quaternion spawnRotation = Quaternion.Euler(0, 0, angle + 90);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            bullet.transform.parent = pivot;
            bullet.GetComponent<PlayerBullet_Script2>().damage = damage;
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

    public void SetRechargeBar(float rechargeBar)
    {
        maxRechargeBar = rechargeBar;
        this.rechargeBar = rechargeBar;
    }
}
