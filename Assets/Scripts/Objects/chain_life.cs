using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chain_life : MonoBehaviour
{
    Transform player;
    Transform _camera;
    [SerializeField] float maxHealth;
    [SerializeField] public float currentHealth;
    bool canBeDamaged => transform.position.x - _camera.position.x <= 12f && timer >= invulnerabilityTime;
    [SerializeField] float invulnerabilityTime;
    float timer;
    private Player_Health playerHealth;

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
      Destroy(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            if (canBeDamaged)
            {
                TakeDamage(player.GetComponent<Player_Shoot>().shootDamage);
                Destroy(other.gameObject);

           }
        }
      
        if (other.CompareTag("Obstacle")|| (other.CompareTag("destructible")))
        {
            Destroy(gameObject);
        }
    }
}

