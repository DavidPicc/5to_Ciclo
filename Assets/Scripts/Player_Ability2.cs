using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ability2 : MonoBehaviour
{
    [SerializeField] float rechargeBar;
    [SerializeField] float maxRechargeBar;
    [SerializeField] Image abilityBar;
    [SerializeField] int maxBullets = 5;
    [SerializeField] Transform pivot;
    [SerializeField] float shieldDistance;
    List<GameObject> bullets = new List<GameObject>();
    public bool endShield;
    public bool activate;
    public float timer;
    public float maxtimer;

    public GameObject bulletPrefab;

    void Start()
    {
        rechargeBar = maxRechargeBar;
        abilityBar.fillAmount = rechargeBar / maxRechargeBar;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.X) && rechargeBar >= maxRechargeBar && !activate)
        {
            SpawnBulletShield();
            activate = true;
        }

        if(activate)
        {
            rechargeBar -= Time.deltaTime;
            abilityBar.fillAmount = rechargeBar/maxRechargeBar;
            if (rechargeBar <= 0)
            {
                PushBullets();
                activate = false;
            }
        }
        else
        {
            rechargeBar += Time.deltaTime;
            abilityBar.fillAmount = rechargeBar / maxRechargeBar;
        }

    }

    void SpawnBulletShield()
    {
        float angleStep = 180 / (maxBullets - 1);
        float angle = -180 / 2;
        for (int i = 0; i < maxBullets; i++)
        {
            float x = shieldDistance * Mathf.Cos(Mathf.Deg2Rad * angle) + pivot.position.x;
            float y = shieldDistance * Mathf.Sin(Mathf.Deg2Rad * angle) + pivot.position.y;
            Vector3 spawnPosition = new Vector3(x, y, pivot.position.z);
            Quaternion spawnRotation = Quaternion.Euler(0, 0, angle + 90);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            bullet.transform.parent = transform;
            angle += angleStep;
            bullets.Add(bullet);
        }
    }

    void PushBullets()
    {
        for(int i = 0; i <bullets.Count; i++)
        {
            if (bullets[i] != null)
            {
                bullets[i].GetComponent<Rigidbody>().AddForce(-bullets[i].transform.up * 4f, ForceMode.Impulse);
                Destroy(bullets[i], 5f);
            }
        }
    }
}
