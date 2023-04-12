using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public Transform _camera;
    public Transform player;
    public Rigidbody rb;

    [Header("Movement")]
    public EnemyMovement enemyMovement;
    public GameObject positionPack;
    Transform[] positions;
    int positionIndex = 0;
    bool locked = false;

    [Header("Shooting")]
    public EnemyBullet typeToShoot;
    public Transform pointShoot;
    private float shootTimer;
    private bool shotBullet;
    public float distanceToShoot;
    private GameObject prefab;
    //bool canShoot => !shotBullet && Mathf.Abs(transform.localPosition.x) <= distanceToShoot;
    bool canShoot => !shotBullet && Vector3.Distance(_camera.position, transform.position) <= distanceToShoot;
    GameObject antiSphere;

    public void Start()
    {
        if(FindObjectOfType<StageMovement>() != null)
        {
            _camera = FindObjectOfType<StageMovement>().transform;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        antiSphere = GetComponentInChildren<SphereCollider>().gameObject;
        antiSphere.SetActive(false);
        if (positionPack != null)
        {
            positions = positionPack.GetComponentsInChildren<Transform>();
        }
        if (typeToShoot)
        {
            prefab = typeToShoot.bulletPrefab;
            shootTimer = typeToShoot.fireRate;
        }
        
        shotBullet = false;
        
    }
    public void Update()
    {
        if (enemyMovement)
        {
            Movement();
        }
        if (typeToShoot)
        {
            Shoot();
        }
    }

    void Movement()
    {
        if (enemyMovement.rotationSpeed > 0) // Si su rotación es mayor a 0.
        {
            RotateAndMove();
        }
        if (enemyMovement.followPlayer) // Sigue al jugador.
        {
            MoveToPlayer();
        }
        else // No sigue al jugador.
        {
            if (positionPack != null) // Alterna su movimiento.
            {
                MoveFromPointToPoint();
            }
            else // No alterna su movimiento.
            {
                if (enemyMovement.xLock == 0) // Sigue de largo hasta desaparecer.
                {
                    MoveToLeft();
                }
                else // Se queda quieto en cierto momento.
                {
                    MoveAndStop();
                }
            }
        }
    }

    void MoveToLeft()
    {
        rb.velocity = transform.right * enemyMovement.velocity;
    }

    void MoveAndStop()
    {
        //if (transform.localPosition.x <= enemyMovement.xLock)
        //if(Vector3.Distance(_camera.position, transform.position) <= enemyMovement.xLock)
        Vector3 _camVec = new Vector3(_camera.position.x, 0, 0);
        Vector3 enemyVec = new Vector3(transform.position.x, 0, 0);

        if (Vector3.Distance(_camVec, enemyVec) <= enemyMovement.xLock)
            {
            //rb.velocity = Vector3.zero;
            locked = true;

        }
        else
        {
            MoveToLeft();
        }

        if (locked)
        {
            transform.parent = _camera;
            rb.velocity = Vector3.zero;
        }
    }

    void RotateAndMove()
    {
        pointShoot.Rotate(0, 0, enemyMovement.rotationSpeed * Time.deltaTime);
    }

    void MoveToPlayer()
    {

        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * enemyMovement.velocity;
    }

    void MoveFromPointToPoint()
    {
        if(Vector3.Distance(transform.position, positions[positionIndex].position) <= 0.05f)
        {
            if (positionIndex < positions.Length - 1)
            {
                positionIndex++;
            }
            else
            {
                positionIndex = 0;
            }
        }
    }

    void Shoot()
    {
        CheckIfShot();
        Anticipation();
        if (canShoot)
        {
            antiSphere.SetActive(false);
            if (typeToShoot.bulletAng > 0)
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
        if (shootTimer <= 0)
        {
            shotBullet = false;
        }

        if (shotBullet)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            shootTimer = typeToShoot.fireRate;
        }
    }

    void Anticipation()
    {
        if(shootTimer <= typeToShoot.anticipation)
        {
            //Play ANTICIPATION animation.
            antiSphere.SetActive(true);
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
            Destroy(bullet, typeToShoot.bulletDisappear);
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
            var bullet = Instantiate(prefab, pointShoot.position, pointShoot.rotation * offsetVector, _camera);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * typeToShoot.bulletSpeed, ForceMode.Impulse);
            //Quaternion offsetVector = Quaternion.Euler(0, 0, angle);
            //var bullet = Instantiate(prefab, pointShoot.position, Quaternion.identity * offsetVector, _camera);
            //bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.right * typeToShoot.bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, typeToShoot.bulletDisappear);
            angle += typeToShoot.bulletAng / (typeToShoot.bulletNumber);
        }

        shotBullet = true;
    }

}
