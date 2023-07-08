using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ability_Fixed: ShopObject
{
    [Header("References")]
    public GameObject shieldObj;
    public GameObject bulletPrefab;
    public Image barUI;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip ShieldSound;

    [Header("Activation")]
    public KeyCode skillKey;
    bool active;
    float rechargeBar;
    public float maxRechargeBar;

    [Header("Shield Charge")]
    public int maxCharge;
    int shieldCharge;

    [Header("Target Selection")]
    public LayerMask enemyLayer;
    public float searchTargetRange;
    public int maxTargetNumber;
    [HideInInspector] public List<Transform> targets;

    [Header("Shield Shoot")]
    public float inBetweenTime;
    public float bulletSpeed;

    [Header("Upgrade System")]
    public int upgradeShieldMaxCharge;
    public int upgradeMaxTargetNumber;

    protected override void Start()
    {
        base.Start();

        //audioManager = GetComponent<AudioSource>();
        rechargeBar = maxRechargeBar;
        SetImageFill();
    }

    void Update()
    {
        Shield();
        if (equipped) SetImageFill();
    }

    void Shield()
    {
        //Activation
        if(!GameManager.instance.isPaused && GameManager.instance.canUseAbilities && Input.GetKeyDown(skillKey) && !active && rechargeBar >= maxRechargeBar && equipped)
        {
            active = true;
            shieldObj.SetActive(true);
            AudioManager.instance.PlaySFX(audioManager, ShieldSound, 0.5f);
        }

        //Deactivation
        if((Input.GetKeyUp(skillKey) || rechargeBar <= 0) && active && !GameManager.instance.isPaused)
        {
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
            if (rechargeBar > 0) rechargeBar -= Time.deltaTime;
        } else
        {
            if (rechargeBar < maxRechargeBar) rechargeBar += Time.deltaTime;
        }
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
            }
        }
    }

    protected override void Shopping()
    {
        base.Shopping();

        if (level > 1)
        {
            maxCharge = upgradeShieldMaxCharge;
            maxTargetNumber = upgradeMaxTargetNumber;
        }
    }
}
