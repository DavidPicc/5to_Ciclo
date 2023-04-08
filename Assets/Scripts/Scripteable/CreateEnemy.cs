using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public EnemyBullet typetoshoot;
    public EnemyMovement enemyMovement;
    private float timer;
    public bool shotBullet;

    Transform _camera;
    
    public void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        shotBullet = true;
    }
    public void Update()
    {
        if (typetoshoot!= null) 
        {
            Shoot();
        }
        
    }


    public  void Shoot()
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
            timer = typetoshoot.timetoshot;
        }

        if (typetoshoot.bulletAng == 0)
        {
            float offset = 0;
            if (shotBullet)
            {
                if (typetoshoot.NumbulletShoot > 1)
                {
                    offset -= (typetoshoot.bulletoffset / 100) / 2;
                }

                for (int i = 0; i < typetoshoot.NumbulletShoot; i++)
                {
                    Vector3 offsetVector = new Vector3(0, offset, 0);
                    var bullet = Instantiate(typetoshoot.BulletPrefab, typetoshoot.shootPoint.transform.position + offsetVector, Quaternion.identity, _camera);
                    bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.right * typetoshoot.bulletSpeed, ForceMode.Impulse);
                    Destroy(bullet, 5f);
                    offset += (typetoshoot.bulletoffset / 100) / (typetoshoot.NumbulletShoot - 1);
                }
                shotBullet = true;
            }
           

            
        }
    }
    
}
