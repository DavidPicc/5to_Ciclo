using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot3 : MonoBehaviour
{
    public bool equipped;
    [SerializeField] public Color lightMaterialEmissionColor;
    bool changedEmissionColor = false;
    public GameObject LanzallamasUI;

    [SerializeField] float bulletSpeed;
    [SerializeField] public float shootDamage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject Gun;
    [SerializeField] public Transform[] shootPoints;
    [SerializeField] public float fireRate;
    [SerializeField] public float maxAngle;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShootSound;
    public float distance;

    float timer;
    bool canShoot => Input.GetKey(KeyCode.Z) && !shotBullet && !GameManager.instance.isPaused && equipped;
    bool shotBullet = false;
    bool playingShoot = false;

    void Start()
    {
        //audioManager = GetComponent<AudioSource>();
        timer = fireRate;
    }


    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            Shoot_Angle();
            if(!playingShoot)
            {
                audioManager.Play();
                audioManager.volume = 1;
                playingShoot = true;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            if(playingShoot)
            {
                audioManager.Stop();
                audioManager.volume = 0;
                playingShoot = false;
            }
        }

        if (equipped == true)
        {
            Gun.SetActive(true);
            LanzallamasUI.SetActive(true);
            audioManager.clip = ShootSound;
            audioManager.loop = true;
            if(!changedEmissionColor)
            {
                GetComponent<Player_Movement>().ChangeEmissionColor(lightMaterialEmissionColor);
                changedEmissionColor = true;
            }

        }
        if (equipped == false)
        {
            Gun.SetActive(false);
            LanzallamasUI.SetActive(false);
            audioManager.loop = false;
            if (changedEmissionColor) changedEmissionColor = false;
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
        var bullet = Instantiate(bulletPrefab, shootPoints[0].position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 8f);
        shotBullet = true;
    }

    void Shoot_Angle()
    {
        float angle = 0;
        angle = Random.Range(-maxAngle, maxAngle);
        Quaternion offsetVector = Quaternion.Euler(0, 0, angle);
        var bullet = Instantiate(bulletPrefab, shootPoints[0].position, shootPoints[0].rotation * offsetVector);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, distance);
        shotBullet = true;
    }
}
