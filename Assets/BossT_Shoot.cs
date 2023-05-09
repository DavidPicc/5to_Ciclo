using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossT_Shoot : MonoBehaviour
{
    public Transform _camera;
    public Transform player;
    public Rigidbody rb;

    [Header("Shooting")]
    public Transform pointShoot;
    public int bulletNumber;
    public float bulletOffset;
    public float bulletSpeed;
    public float shootTimer;
    float timer;
    private bool shotBullet;
    public float distanceToShoot;
    public GameObject prefab;
    //bool canShoot => !shotBullet && Mathf.Abs(transform.localPosition.x) <= distanceToShoot;
    bool canShoot => !shotBullet && Vector3.Distance(_camera.position, transform.position) <= distanceToShoot;
    GameObject antiSphere;
    void Start()
    {
        if (FindObjectOfType<StageMovement>() != null)
        {
            _camera = FindObjectOfType<StageMovement>().transform;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        //antiSphere = GetComponentInChildren<SphereCollider>().gameObject;
        //antiSphere.SetActive(false);

        shotBullet = false;
        timer = shootTimer;
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if(GetComponent<BossT_Animations>() != null)
        {
            if(GetComponent<BossT_Animations>().bossIn)
            {
                if(SituationManager.instance.wave >= SituationManager.instance.bossWave+3)
                {
                    CheckIfShot();
                    //Anticipation();
                }

            }
        }
        
    }

    void CheckIfShot()
    {
        if (!shotBullet)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if (canShoot)
                {
                    ShootLinear();
                    timer = shootTimer;
                    shotBullet = false;
                }
            }
        }
    }

    //void Anticipation()
    //{
    //    if (shootTimer <= typeToShoot.anticipation)
    //    {
    //        //Play ANTICIPATION animation.
    //        antiSphere.SetActive(true);
    //    }
    //}

    public void ShootLinear()
    {

        float offset = 0;
        if (bulletNumber > 1)
        {
            offset -= (bulletOffset) / 2;
        }

        for (int i = 0; i < bulletNumber; i++)
        {
            Vector3 offsetVector = new Vector3(0, offset, 0);
            var bullet = Instantiate(prefab, pointShoot.position + offsetVector, Quaternion.identity, _camera);
            bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.right * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 8f);
            offset += (bulletOffset) / (bulletNumber - 1);
        }

        shotBullet = true;
    }
}
