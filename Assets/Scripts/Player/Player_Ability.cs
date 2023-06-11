using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ability : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] float rechargeBar;
    [SerializeField] float maxRechargeBar;
    [SerializeField] Image abilityBar;

    public bool endShield;
    public bool activate;
    public float timer;
    public float maxtimer;
    private int cant=10;

    public float tk;
    public float takeDmg;
    public int damageCounter;

    public float range = 10f;
    public LayerMask targetLayer;
    private bool isTargetDestroyed;

    public Transform currentTarget;
    private List<Transform> previousTargets = new List<Transform>();
    public GameObject bulletPrefab;

    public float points;

    public bool equipped;

    [Header("Upgrade System")]
    [SerializeField] int levelAbb;
    public float upgradedPoints;
    public Transform currentTarget2;
    private bool isTargetDestroyed2;

    private void OnEnable()
    {
        GameManager.onShopApply += Shopping;
    }

    private void OnDisable()
    {
        GameManager.onShopApply -= Shopping;
    }

    void Start()
    {
        rechargeBar = maxRechargeBar;
        abilityBar.fillAmount = rechargeBar / maxRechargeBar;
        levelAbb = 1;
        Shopping();
    }
    private void FixedUpdate()
    {
        abilityBar.fillAmount = rechargeBar / maxRechargeBar;
        ActivateShield();
        if (currentTarget == null && levelAbb <= 1)
        {
            FindNewTarget();
        } else if((currentTarget == null || currentTarget2 == null || currentTarget.transform.position.x < transform.position.x || currentTarget2.transform.position.x < transform.position.x) && levelAbb > 1){
            Find2NewTarget();
        }
    }
    public  void ActivateShield()
    {
        if (Input.GetKey(KeyCode.X) && equipped)
        {
            activate = true;

            tk = Mathf.Min(takeDmg, points);
        }
        else
        {
            activate = false;
            takeDmg = 0;
            Instantia();
        }
        if (activate)
        {
            rechargeBar -= Time.deltaTime;

            if (!endShield)
            {
                if (rechargeBar >= 0)
                {
                    shieldObj.SetActive(true);  
                }
                else
                {
                    endShield = true;
                    shieldObj.SetActive(false);
                    
                }
            }
        }
        else
        { 
            shieldObj.SetActive(false);
            rechargeBar += Time.deltaTime;
            if (rechargeBar > maxRechargeBar)
            {
                rechargeBar = maxRechargeBar;
            }
            abilityBar.fillAmount = rechargeBar / maxRechargeBar;
        }
        if (endShield)
        {
            timer += Time.deltaTime;
            if (timer >= maxtimer)
            {
                endShield = false;
                timer = 0;
            }
        }     
    }
    void Instantia()
    {
        InvokeRepeating("Shoot", 0.05f, cant);
    }
    void Shoot()
    {

        if (damageCounter >= 5 || rechargeBar <= 0)
        {
            if (damageCounter >= 5)
            {
                damageCounter = 0;
            }

            CancelInvoke("Shoot");
            return;
        }
        if (tk > 0 && currentTarget != null && !isTargetDestroyed && levelAbb <= 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation); // instancia la bala en la posición y rotación del objeto actual
            bullet.transform.LookAt(currentTarget); // apunta la posición de la bala hacia la posición del enemigo
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * 30, ForceMode.Impulse);
            tk--;
            damageCounter++;
            if (damageCounter >= points)
            {
              CancelInvoke("Shoot");
            }
        } else if(tk > 0 && ((currentTarget != null && !isTargetDestroyed) || (currentTarget2 != null && !isTargetDestroyed2)) && levelAbb > 1)
        {
            if(currentTarget != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation); // instancia la bala en la posición y rotación del objeto actual
                bullet.transform.LookAt(currentTarget); // apunta la posición de la bala hacia la posición del enemigo
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bullet.transform.forward * 30, ForceMode.Impulse);
            }

            if(currentTarget2 != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation); // instancia la bala en la posición y rotación del objeto actual
                bullet.transform.LookAt(currentTarget2); // apunta la posición de la bala hacia la posición del enemigo
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bullet.transform.forward * 30, ForceMode.Impulse);
            }

            tk--;
            damageCounter++;
            if (damageCounter >= points)
            {
                CancelInvoke("Shoot");
            }
        }
    }
    private void FindNewTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range, targetLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider targetCollider in targets)
        {
            Transform targetTransform = targetCollider.transform;

            if (previousTargets.Contains(targetTransform))
            {
                // Skip targets that have already been selected
                continue;
            }
            float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

            if (distanceToTarget < closestDistance && targetTransform.position.x > transform.position.x)
            {
                closestDistance = distanceToTarget;
                closestTarget = targetTransform;
            }
        }
        currentTarget = closestTarget;

        if (currentTarget == null)
        {
            isTargetDestroyed = true;
        }
        else
        {
            isTargetDestroyed = false;
        }

        previousTargets.Clear();
    
    //   currentTarget = closestTarget;
    //   previousTargets.Clear();
    }

    private void Find2NewTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range, targetLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider targetCollider in targets)
        {
            Transform targetTransform = targetCollider.transform;

            if (previousTargets.Contains(targetTransform))
            {
                // Skip targets that have already been selected
                continue;
            }
            float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

            if (distanceToTarget < closestDistance && targetTransform.position.x > transform.position.x)
            {
                closestDistance = distanceToTarget;
                closestTarget = targetTransform;
            }
        }
        currentTarget = closestTarget;

        closestDistance = Mathf.Infinity;
        closestTarget = null;

        foreach (Collider targetCollider in targets)
        {
            Transform targetTransform = targetCollider.transform;

            if (previousTargets.Contains(targetTransform))
            {
                // Skip targets that have already been selected
                continue;
            }
            float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

            if (distanceToTarget < closestDistance && targetTransform.position.x > transform.position.x && targetTransform != currentTarget)
            {
                closestDistance = distanceToTarget;
                closestTarget = targetTransform;
            }
        }
        currentTarget2 = closestTarget;

        if (currentTarget == null)
        {
            isTargetDestroyed = true;
        }
        else
        {
            isTargetDestroyed = false;
        }

        if (currentTarget2 == null)
        {
            isTargetDestroyed2 = true;
        }
        else
        {
            isTargetDestroyed2 = false;
        }

        previousTargets.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetLayer == (targetLayer | (1 << other.gameObject.layer)))
        {
            if (other.transform == currentTarget)
            {

                return;
            }

            previousTargets.Add(currentTarget);
            currentTarget = null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        if (currentTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, currentTarget.position);
            Gizmos.DrawWireSphere(currentTarget.position, 1f);
        }
    }
    public void UpHability()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            maxRechargeBar += 100;
        }
    }

    void Shopping()
    {
        if (UpgradeTracker.instance.levels.ContainsKey("ReflectorShield"))
        {
            levelAbb = UpgradeTracker.instance.levels["ReflectorShield"];
        }

        if (levelAbb > 1)
        {
            points = upgradedPoints;
        }

        equipped = UpgradeTracker.instance.equippedSkill == "ReflectorShield";
    }
}
