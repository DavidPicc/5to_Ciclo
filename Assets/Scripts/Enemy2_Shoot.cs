using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_Shoot : MonoBehaviour
{
    public enum ShotType
    {
        lineal,
        angular
    }
    public ShotType shotType;
    [SerializeField] int numberOfBullets;
    [SerializeField] float shootAngle;
    [SerializeField] float bulletOffset;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
    Transform player;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate;
    float timer;

    bool canShoot => !shotBullet && Mathf.Abs(transform.localPosition.x) <= distanceToShoot;
    public float distanceToShoot;
    bool shotBullet = false;

    Transform _camera;
    void Start()
    {
        timer = fireRate;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = FindObjectOfType<StageMovement>().transform;
    }


    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            //Shoot_Normal();
            switch(shotType)
            {
                case ShotType.lineal:
                    Shoot_Linear();
                    break;
                case ShotType.angular:
                    Shoot_Angular();
                    break;
            }
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

    void Shoot_Linear()
    {
        float offset = 0;
        if(numberOfBullets > 1)
        {
            offset -= (bulletOffset / 100) / 2;
        }
        
        for(int i = 0; i < numberOfBullets; i++)
        {
            Vector3 offsetVector = new Vector3(0, offset, 0);
            var bullet = Instantiate(bulletPrefab, shootPoint.position + offsetVector, Quaternion.identity, _camera);
            bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 5f);
            offset += (bulletOffset / 100) / (numberOfBullets - 1);
        }

        shotBullet = true;
    }

    void Shoot_Angular()
    {
        float angle = 0;
        if (numberOfBullets > 1)
        {
            angle -= (shootAngle) / 2;
        }

        for (int i = 0; i < numberOfBullets; i++)
        {
            Quaternion offsetVector = Quaternion.Euler(0, 0, angle);
            var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity * offsetVector, _camera);
            bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 5f);
            angle += shootAngle / (numberOfBullets - 1);
        }

        shotBullet = true;
    }
}
