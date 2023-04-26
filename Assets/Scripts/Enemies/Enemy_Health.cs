using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy_Health : MonoBehaviour
{
    Transform player;
    Transform _camera;
    [SerializeField] float maxHealth;
    [SerializeField] public float currentHealth;
    public bool canBeDamaged => transform.position.x - _camera.position.x <= 12f && timer >= invulnerabilityTime;
    [SerializeField] float invulnerabilityTime;
    float timer;

    [SerializeField] GameObject pointsPrefab;
    [SerializeField] int enemyValueInPoints;

    bool spawnedPoints = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = FindObjectOfType<StageMovement>().transform;

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
        Destroy(this);
        Destroy(gameObject, 0.2f);
        if(!spawnedPoints)
            SpawnPoints();
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
        if (other.CompareTag("PlayerBullet"))
        {
            if (canBeDamaged)
            {
                TakeDamage(player.GetComponent<Player_Shoot>().shootDamage);

            }
            Destroy(other.gameObject, 1f);
        }
        if (other.CompareTag("Obstacle") || other.CompareTag("weigh"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Explosion"))
        {
            if (canBeDamaged)
            {
                TakeDamage(100);
            }
           
        }
    }
}
