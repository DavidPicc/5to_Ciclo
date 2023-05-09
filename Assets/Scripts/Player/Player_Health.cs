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
    bool canBeDamaged = true;

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
            canBeDamaged = true;
            timer = invulnerabilityTime;
        }

        if (!canBeDamaged)
        {
            timer -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if(canBeDamaged)
        {
            FindObjectOfType<CameraShake>().ShakeCamera(1.5f * (damage / 2), 0.1f);

            currentHealth -= damage;
            UpdateHealthBar();
            if (currentHealth <= 0)
            {
                Death();
            }
            Debug.Log("Player has been damaged!!!!");
            canBeDamaged = false;
        }
    }
    public void Death()
    {
        //Destroy(gameObject, 0.2f);
        //Destroy(this);

        // TUTORIAL
        if(FindObjectOfType<BossT_Shoot>() != null && SituationManager.instance.wave >= SituationManager.instance.bossWave+3)
        {
            gameObject.SetActive(false);
            TutorialManager.instance.bossDeathTutorial.SetActive(true);
            Debug.Log("NIVEL 1 DESBLOQUEADO");
        }
        else
        {
            Invoke("SetPlayerDeath", 0.3f);
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weigh"))
        {
            TakeDamage(1);
        }


    }
}
