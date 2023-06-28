using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Enemy_Health : MonoBehaviour
{
    Transform player;
    Transform _camera;
    [SerializeField] float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip DeathSound;

    public bool IsEnemy = true;
    public bool canBeDamaged => transform.position.x - _camera.position.x <= 12f && timer >= invulnerabilityTime;
    [SerializeField] float invulnerabilityTime;
    float timer;

    [SerializeField] GameObject pointsPrefab;
    [SerializeField] int enemyValueInPoints;
    public Player_Health playerHealth;

    bool spawnedPoints = false;

    [SerializeField] bool obstacle = false;
    [SerializeField] public bool invulnerable = false;
    [SerializeField] GameObject vfxexplosion;

    [Header("Boss")]
    public bool IsBoss = false;
    public GameObject Form1;
    public GameObject Form2;
    [SerializeField] float halfthealt;
    [SerializeField] float moveSpeed = 2f;
    public Transform[] movePoints;
    private int currentMovePointIndex = 0;
    public Phase1 phase1;
    public Phase2 phase2;
    public Phase3 phase3;
    public Phase4 phase4;
    public string FinishGame;
    void Start()
    {
        playerHealth = FindAnyObjectByType<Player_Health>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = FindObjectOfType<StageMovement>().transform;
        audioManager = GetComponent<AudioSource>();

        timer = invulnerabilityTime;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (timer <= 0)
        {
            timer = invulnerabilityTime;
        }

        if(!canBeDamaged)
        {
            timer -= Time.deltaTime;
        }

        if (IsBoss == true && currentHealth == 0)
        {
            SceneManager.LoadScene(FinishGame);
        }

        else if (IsBoss == true && currentHealth <= 250)
        {
            phase1.enabled = false;
            phase2.enabled = false;
            phase3.enabled = false;
            phase4.enabled = true;
            MoveToNextPoint();
        }

        else if (IsBoss == true && currentHealth <= 500)
        {
            phase1.enabled = false;
            phase2.enabled = false;
            phase3.enabled = true;
            phase4.enabled = false;
            MoveToNextPoint();
            Form1.SetActive(false);
            Form2.SetActive(true);
        }

        else if (IsBoss == true && currentHealth <= 750)
        {
            phase1.enabled = false;
            phase2.enabled = true;
            phase3.enabled = false;
            phase4.enabled = false;
        }

        else if (IsBoss == true && currentHealth <= 1000)
        {
            phase1.enabled = true;
            phase2.enabled = false;
            phase3.enabled = false;
            phase4.enabled = false;
            Form1.SetActive(true);
            Form2.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        if(currentHealth > damage)
        {
            currentHealth -= damage;
        }
        else
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("MURIO " + name);
        if (GetComponent<Enemy4_ShootExplode>() != null)
        {
            GetComponent<Enemy4_ShootExplode>().ShootAngular();
        }

        //audioManager.PlayOneShot(DeathSound);
        Destroy(gameObject, 0.2f);
        if (!spawnedPoints)
            SpawnPoints();
        var death = Instantiate(vfxexplosion, transform.position, Quaternion.identity);
        death.AddComponent<AudioSource>();
        death.GetComponent<AudioSource>().outputAudioMixerGroup = FindObjectOfType<AudioManager>().sfxGroup;
        death.GetComponent<AudioSource>().volume = 1.0f;
        death.GetComponent<AudioSource>().PlayOneShot(DeathSound);
        Destroy(death, 3f);

        Destroy(this);
    }

    void SpawnPoints()
    {
        spawnedPoints = true;
       
        for (int i = 0; i < enemyValueInPoints; i++)
        {
            var point = Instantiate(pointsPrefab, transform.position, Quaternion.identity);
        }
       
    }

    private void MoveToNextPoint()
    {
        if (currentMovePointIndex >= movePoints.Length)
        {
            currentMovePointIndex = 0;
        }

        Transform nextMovePoint = movePoints[currentMovePointIndex];

        transform.position = Vector3.MoveTowards(transform.position, nextMovePoint.position, moveSpeed * Time.deltaTime);

        if (transform.position == nextMovePoint.position)
        {
            currentMovePointIndex++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!invulnerable)
        {
            //if (other.CompareTag("PlayerBullet"))
            //{
            //    if (canBeDamaged)
            //    {
            //        TakeDamage(player.GetComponent<Player_Shoot>().shootDamage);

            //    }
            //    Destroy(other.gameObject, 1f);
            //}
            if (IsEnemy == true && other.CompareTag("Obstacle") || other.CompareTag("weigh") || other.CompareTag("destructible"))
            {
                //Destroy(gameObject);
                if (!obstacle)
                {
                    TakeDamage(100);
                }
            }
            if (other.CompareTag("Explosion"))
            {
                if (canBeDamaged)
                {
                    TakeDamage(3000);
                }
            }
            if (other.CompareTag("Player"))
            {
                if (canBeDamaged)
                {
                    playerHealth.TakeDamage(1);
                }
            }
        }
    }
}
