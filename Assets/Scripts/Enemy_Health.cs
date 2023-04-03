using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy_Health : MonoBehaviour
{
    Transform player;
    Transform _camera;
    public float maxHealth;
    public float health;
    bool canBeDamaged => transform.position.x - _camera.position.x <= 8f && timer >= invulnerabilityTime;
    public float invulnerabilityTime;
    float timer;

    public GameObject pointsPrefab;
    public int enemyValueInPoints;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = FindObjectOfType<StageMovement>().transform;

        timer = invulnerabilityTime;
        health = maxHealth;
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
        if(health > damage)
        {
            health -= damage;
        }
        else
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject, 0.2f);
        for(int i= 0; i < enemyValueInPoints; i++)
        {
            var point = Instantiate(pointsPrefab, transform.position, Quaternion.identity);
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            Vector3 randomVector = new Vector3(randomX,randomY, 0);
            point.GetComponent<Rigidbody>().AddForce(randomVector * 4f, ForceMode.Impulse);
        }
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            if(canBeDamaged)
            {
                TakeDamage(player.GetComponent<Player_Shoot>().shootDamage);
                Destroy(other.gameObject);
            }
        }
    }
}
