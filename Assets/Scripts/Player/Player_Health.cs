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
    [SerializeField] public bool canDie = true;

    [SerializeField] Image healthFillBar;

    [Header("Crushed")]
    public int crushed = 0;

    void Start()
    {
        timer = invulnerabilityTime;
        currentHealth = maxHealth;

        //transform.position = CheckPointScript.savedPoint;


        if (FindObjectOfType<TutorialManager>() != null)
        {
            canDie = false;
        }
        else
        {
            canDie = true;
        }
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

        if(Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage(1);
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
                if(canDie)
                {
                    Death();
                }
            }
            if(!canDie)
            {
                if(currentHealth <= 1)
                {
                    DialogueScript.instance.SetDialogue(FindAnyObjectByType<FriendScript>().RandomTalk());
                    currentHealth = maxHealth;
                    UpdateHealthBar();
                }
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
            TutorialManager.instance.bossDeathTutorialTransition.SetActive(true);
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
        if (other.CompareTag("weigh") || other.CompareTag("Explosion") || other.CompareTag("Enemy"))
        {
            if (canBeDamaged)
            {
                TakeDamage(1);
            }
        }
    }
}
