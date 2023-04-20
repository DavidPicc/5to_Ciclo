using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] public float invulnerabilityTime;
    float timer;
    bool canBeDamaged => timer >= invulnerabilityTime && !GetComponent<Player_Dodge>().isDodging;

    [SerializeField] Image healthFillBar;

        void Start()
    {
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
        FindObjectOfType<CameraShake>().ShakeCamera(1.5f * (damage/2), 0.1f);

        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        //Destroy(gameObject, 0.2f);
        //Destroy(this);
        Invoke("SetPlayerDeath", 0.3f);
    }

    public void SetPlayerDeath()
    {
        gameObject.SetActive(false);
        GameManager.instance.DeathMenu();
    }

    void UpdateHealthBar()
    {
        healthFillBar.fillAmount = currentHealth / maxHealth;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("EnemyBullet") || (other.CompareTag("weigh")))
    //    {
    //        if (canBeDamaged)
    //        {
    //            TakeDamage(1);
    //            Destroy(other.gameObject);
    //        }
    //    }

    //    if (other.CompareTag("Obstacle"))
    //    {
    //        if (canBeDamaged)
    //        {
    //            TakeDamage(1);
    //        }
    //    }

    //    if (other.CompareTag("Explosion"))
    //    {
    //        if (canBeDamaged)
    //        {
    //            TakeDamage(1);
    //        }
    //    }
    //}
}
