using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ability_Fixed: MonoBehaviour
{
    public bool equipped;

    [Header("References")]
    public GameObject EscudoReflectorUI;
    public GameObject shieldObj;
    public GameObject bulletPrefab;
    public Image barUI;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShieldSound;
    [SerializeField] public AudioClip ShieldLowSound;

    [Header("Activation")]
    bool abilityChargedSoundPlayed = false;
    [SerializeField] public AudioClip AbilityChargedSound;
    [SerializeField] public AudioSource audioManagerSecondary;
    [SerializeField] public AudioClip ActivationSound;
    public KeyCode skillKey;
    bool active;
    float rechargeBar;
    public float maxRechargeBar;

    [Header("Shield Charge")]
    public int maxCharge;
    int shieldCharge;
    [SerializeField] public AudioClip AbsorbSound;

    [Header("Target Selection")]
    public LayerMask enemyLayer;
    public float searchTargetRange;
    public int maxTargetNumber;
    [HideInInspector] public List<Transform> targets;

    [Header("Shield Shoot")]
    public float inBetweenTime;
    public float bulletSpeed;

    void Start()
    {
        //audioManager = GetComponent<AudioSource>();
        rechargeBar = maxRechargeBar;
        SetImageFill();
    }

    void Update()
    {
        Shield();
        //if (equipped) SetImageFill();
    }

    void Shield()
    {
        //Activation
        if(!GameManager.instance.isPaused && GameManager.instance.canUseAbilities && Input.GetKeyDown(skillKey) && !active && rechargeBar >= maxRechargeBar && equipped)
        {
            AudioManager.instance.PlaySFX(audioManagerSecondary, ActivationSound, 1.0f);
            PlayShieldSound();
            active = true;
            shieldObj.SetActive(true);
            abilityChargedSoundPlayed = false;
            //AudioManager.instance.PlaySFX(audioManager, ShieldSound, 0.5f);
        }

        //Deactivation
        if((Input.GetKeyUp(skillKey) || rechargeBar <= 0) && active && !GameManager.instance.isPaused)
        {
            StopShieldSound();
            active = false;
            shieldObj.SetActive(false);
            if (shieldCharge > 0)
            {
                StartCoroutine(Shoot(shieldCharge));
                shieldCharge = 0;
            }
        }

        //Timer
        if (active)
        {
            rechargeBar -= Time.deltaTime;
            if (equipped) barUI.fillAmount = rechargeBar / maxRechargeBar;
            if (rechargeBar <= maxRechargeBar/3)
            {
                audioManager.clip = ShieldLowSound;
            }
            Debug.Log(rechargeBar);
            if (rechargeBar > 0) rechargeBar -= Time.deltaTime;
        }
        else
        {
            rechargeBar += Time.deltaTime;
            rechargeBar = Mathf.Clamp(rechargeBar, 0f, maxRechargeBar);
            if (equipped) barUI.fillAmount = rechargeBar / maxRechargeBar;
            if (rechargeBar >= maxRechargeBar && !abilityChargedSoundPlayed)
            {
                AudioManager.instance.PlaySFX(audioManager, AbilityChargedSound, 1.0f);
                abilityChargedSoundPlayed = true;
            }
        }

        if (equipped == true)
        {
            EscudoReflectorUI.SetActive(true);
        }
        if (equipped == false)
        {
            EscudoReflectorUI.SetActive(false);
        }
    }

    void PlayShieldSound()
    {
        audioManager.volume = 1;
        audioManager.clip = ShieldSound;
        audioManager.loop = true;
        audioManager.Play();
    }

    void StopShieldSound()
    {
        audioManager.loop = false;
        audioManager.Stop();
    }

    IEnumerator Shoot(int charges)
    {
        for(int i = 0; i < charges; i++)
        {
            if(!CheckOneTargetActive())
            {
                if (!FindTargets()) break;
            }

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.LookAt(targets[i % targets.Count]);
            
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

            if (i < charges - 1) yield return new WaitForSeconds(inBetweenTime);
        }

        yield return null;
    }

    void SetImageFill()
    {
        barUI.fillAmount = Mathf.Clamp(rechargeBar / maxRechargeBar, 0f, 1f);
    }

    bool FindTargets()
    {
        List<Transform> orderedColliders = new List<Transform>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, searchTargetRange, enemyLayer);
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            Transform colliderTransform = collider.transform;

            if (colliderTransform == null) continue;

            float distance = Vector3.Distance(transform.position, colliderTransform.position);

            if (distance <= closestDistance && colliderTransform.position.x > transform.position.x)
            {
                orderedColliders.Add(colliderTransform);
                closestDistance = distance; 
            }
        }

        //Select targets
        orderedColliders.Reverse();
        targets = new List<Transform>();
        for(int i = 0; i < maxTargetNumber && i < orderedColliders.Count; i++)
        {
            targets.Add(orderedColliders[i]);
        }

        //Check if succesful search
        return targets.Count > 0;
    }

    bool CheckOneTargetActive()
    {
        targets.RemoveAll(t => t == null || t.position.x < transform.position.x);

        return targets.Count > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            if (active)
            {
                Destroy(other.gameObject);
                shieldCharge = Mathf.Min(++shieldCharge, maxCharge);
                AudioManager.instance.PlaySFX(audioManagerSecondary, AbsorbSound, 1.0f);
            }
        }
    }

    public void SetRechargeBar(float rechargeBar)
    {
        maxRechargeBar = rechargeBar;
        this.rechargeBar = rechargeBar;
    }
}
