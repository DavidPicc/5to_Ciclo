using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public EnemyBullet typeToShoot;
    public EnemyMovement enemyMovement;
    public Transform pointShoot;
    private float timer;
    private bool shotBullet;
    public float distanceToShoot;
    private GameObject prefab;

    public Transform _camera;
    bool canShoot => !shotBullet && Mathf.Abs(transform.localPosition.x) <= distanceToShoot;
    public void Start()
    {
        prefab = typeToShoot.bulletPrefab;
        //_camera = FindObjectOfType<StageMovement>().transform;
        shotBullet = false;
        timer = typeToShoot.fireRate;
    }
    public void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            if ( typeToShoot.bulletAng>0)
            {
                ShootAngular();
            }
            else
            {
                ShootLinear();
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
            timer = typeToShoot.fireRate;
        }
    }

    public  void ShootLinear()
    {

        float offset = 0;
        if (typeToShoot.bulletNumber > 1)
        {
            offset -= (typeToShoot.bulletOffset / 100) / 2;
        }

        for (int i = 0; i < typeToShoot.bulletNumber; i++)
        {
            Vector3 offsetVector = new Vector3(0, offset, 0);
            var bullet = Instantiate(prefab, pointShoot.position + offsetVector, Quaternion.identity, _camera);
            bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.right * typeToShoot.bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 5f);
            offset += (typeToShoot.bulletOffset / 100) / (typeToShoot.bulletNumber - 1);
        }

        shotBullet = true;
    }
    void ShootAngular()
    {
        float angle = 0;
        if (typeToShoot.bulletNumber > 1)
        {
            angle -= (typeToShoot.bulletAng) / 2;
        }

        for (int i = 0; i < typeToShoot.bulletNumber; i++)
        {
            Quaternion offsetVector = Quaternion.Euler(0, 0, angle);
            var bullet = Instantiate(prefab, pointShoot.position, Quaternion.identity * offsetVector, _camera);
            bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.right * typeToShoot.bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 5f);
            angle += typeToShoot.bulletAng / (typeToShoot.bulletNumber - 1);
        }

        shotBullet = true;
    }

}
