using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    private const float REAL_MAX_HEALTH = 5f;

    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] public float invulnerabilityTime;
    float timer;
    bool canBeDamaged = true;
    [SerializeField] public bool canDie = true;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip DeathSound;
    [SerializeField] public AudioClip HitSound;

    [SerializeField] Image healthFillBar;

    [Header("Caritas")]
    public Image caritaTriste;
    public Image caritaNormal;
    public Image caritaFeliz;
    public Image caritaCorona;
    public Image caritaFachera;
    public GameObject VfxlowLife;

    [Header("Mejoras")]
    public GameObject Mejora1H;
    public GameObject Mejora2aH;
    public GameObject Mejora2bH;
    public GameObject MejoraI1;
    public GameObject MejoraI2;
    public GameObject MejoraI3;

    [Header("Crushed")]
    public int crushed = 0;

    void Start()
    {
        //audioManager = GetComponent<AudioSource>();
        timer = invulnerabilityTime;
        currentHealth = maxHealth;
        UpdateHealthBar();
        ActualizarCaritas();
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

        if (Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage(1);
        }

        if(maxHealth == 4)
        {
            Mejora1H.SetActive(true);
        }

        if (maxHealth == 5)
        {
            Mejora2aH.SetActive(true);
            Mejora2bH.SetActive(true);
        }

        if (invulnerabilityTime == 2)
        {
            MejoraI1.SetActive(false);
            MejoraI2.SetActive(true);
        }

        if (invulnerabilityTime == 2.5)
        {
            MejoraI2.SetActive(false);
            MejoraI3.SetActive(true);
        }
    }

    public void TakeDamage(float damage)
    {
        if (canBeDamaged)
        {
            FindObjectOfType<CameraShake>().ShakeCamera(1.5f * (damage), 0.1f);
            audioManager.PlayOneShot(HitSound);
            currentHealth -= damage;
            UpdateHealthBar();
            if (currentHealth <= 0)
            {
                if (canDie)
                {
                    Death();
                }
            }
            if (!canDie)
            {
                if (currentHealth <= 1)
                {
                    FindAnyObjectByType<FriendScript>().HealPlayer();
                }
            }
            ActualizarCaritas();
            Debug.Log("Player has been damaged!!!!");
            canBeDamaged = false;
        }
    }

    public void GetFullHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        ActualizarCaritas();
        Debug.Log("Player has been fully healed!");
    }
    public void Death()
    {
        // TUTORIAL
        if (canDie && FindObjectOfType<TutorialManager>() != null)
        {
            GameManager.instance.FinishedLevel();
        }
        else
        {
            //Invoke("SetPlayerDeath", 0.03f);
            SetPlayerDeath();
        }
    }

    public void SetPlayerDeath()
    {
        GameObject deathSound = new GameObject();
        deathSound.name = "DeathSound";
        deathSound.AddComponent<AudioSource>();
        deathSound.GetComponent<AudioSource>().outputAudioMixerGroup = AudioManager.instance.sfxGroup;
        deathSound.GetComponent<AudioSource>().PlayOneShot(DeathSound);

        gameObject.SetActive(false);
        GameManager.instance.DeathMenu();
    }

    public void UpdateHealthBar()
    {
        healthFillBar.fillAmount = currentHealth / REAL_MAX_HEALTH;
    }
    void ActualizarCaritas()
    {
        caritaTriste.gameObject.SetActive(currentHealth == 1);
        VfxlowLife.gameObject.SetActive(currentHealth == 1);
        caritaNormal.gameObject.SetActive(currentHealth == 2);
        caritaFeliz.gameObject.SetActive(currentHealth == 3);
        caritaCorona.gameObject.SetActive(currentHealth == 4);
        caritaFachera.gameObject.SetActive(currentHealth >= 5);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weigh") || other.CompareTag("Explosion")) //|| other.CompareTag("Enemy"))
        {
            if (canBeDamaged)
            {
                TakeDamage(1);
            }
        }
    }

    public void SetHealth(float health)
    {
        maxHealth = health;
        currentHealth = health;
        UpdateHealthBar();
        ActualizarCaritas();
    }
}
