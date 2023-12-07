using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chain_life : MonoBehaviour
{
    Transform _camera;
    [SerializeField] float maxHealth;
    [SerializeField] public float currentHealth;
    bool canBeDamaged => transform.position.x - _camera.position.x <= 45f && timer >= invulnerabilityTime;
    [SerializeField] float invulnerabilityTime;
    float timer;
    public GameObject explosionPrefab;

    void Start()
    {
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
        GameObject impact = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 1f);
        Destroy(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            if (canBeDamaged)
            {
                GameObject impact = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(impact, 1f);
                TakeDamage(10);
                Destroy(other.gameObject);

           }
        }
      
        if (other.CompareTag("Obstacle")|| (other.CompareTag("destructible")))
        {
            Destroy(gameObject);
        }
    }
}

