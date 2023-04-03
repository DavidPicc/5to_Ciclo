using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    public float shootSpeed;
    public float shootDamage;
    public GameObject bulletPrefab;
    public Transform[] shootPoints;
    public float fireRate;
    float timer;
    bool canShoot => Input.GetMouseButton(0) && !shotBullet;
    bool shotBullet = false;
    int shotIndex = 0;
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
        //for (int i = 0; i < shootPoints.Length; i++)
        //{
        //    var bullet = Instantiate(bulletPrefab, shootPoints[shotIndex].position, Quaternion.identity);
        //    bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * shootSpeed, ForceMode.Impulse);
        //    Destroy(bullet, 2f);
        //}

        var bullet = Instantiate(bulletPrefab, shootPoints[shotIndex].position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * shootSpeed, ForceMode.Impulse);
        Destroy(bullet, 2f);

        shotBullet = true;

        if (shotIndex < 1)
        {
            shotIndex++;
        }
        else shotIndex = 0;
    }
}
