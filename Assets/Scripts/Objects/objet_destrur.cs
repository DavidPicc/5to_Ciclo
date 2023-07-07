using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objet_destrur : MonoBehaviour
{
    Transform player;
    Transform _camera;
    [SerializeField] float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip DeathSound;
    public bool canBeDamaged => transform.position.x - _camera.position.x <= 12f && timer >= invulnerabilityTime;
    [SerializeField] float invulnerabilityTime;
    float timer;

    [SerializeField] GameObject pointsPrefab;
    [SerializeField] int enemyValueInPoints;

    bool spawnedPoints = false;
    [SerializeField] GameObject vfxexplosion;
    [SerializeField] bool obstacle = false;
    [SerializeField] public bool invulnerable = false;
    void Start()
    {
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

        if (!canBeDamaged)
        {
            timer -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > damage)
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

        Destroy(gameObject, 0.2f);
        if (!spawnedPoints)
            SpawnPoints();
        var death = Instantiate(vfxexplosion, transform.position, Quaternion.identity);
        death.AddComponent<AudioSource>();
        death.GetComponent<AudioSource>().outputAudioMixerGroup = FindObjectOfType<AudioManager>().sfxGroup;
        death.GetComponent<AudioSource>().volume = 1.0f;
        death.GetComponent<AudioSource>().PlayOneShot(DeathSound);
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
    private void OnTriggerEnter(Collider other)
    {
        if (!invulnerable)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                if (canBeDamaged)
                {
                    TakeDamage(player.GetComponent<Player_Shoot>().shootDamage);
                }
                Destroy(other.gameObject);

            }

            if (CompareTag("Obstacle") || other.CompareTag("weigh"))
            {
                Destroy(gameObject);
                if (!obstacle)
                {
                    TakeDamage(100);
                }
            }
            if (other.CompareTag("Explosion"))
            {
                if (canBeDamaged)
                {
                    TakeDamage(4000);
                }
            }
            if (other.CompareTag("PlayerBullet"))
            {
                if (canBeDamaged)
                {
                    TakeDamage(4000);
                }
            }
        }
    }
}
