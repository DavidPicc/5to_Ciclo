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
    //private int cant=10;

    public float tk;
    public float takeDmg;


   // public float range = 10f;
   // public LayerMask targetLayer;
    

    //public Transform currentTarget;
    //private List<Transform> previousTargets = new List<Transform>();
    //public GameObject bulletPrefab;

    void Start()
    {
        rechargeBar = maxRechargeBar;
        abilityBar.fillAmount = rechargeBar / maxRechargeBar;
    }

    private void FixedUpdate()
    {
        abilityBar.fillAmount = rechargeBar / maxRechargeBar;
        ActivateShield();
        //if (currentTarget == null)
        //{
        //    FindNewTarget();
        //}
    }
    public  void ActivateShield()
    {
        if (Input.GetKey(KeyCode.X))
        {
            activate = true;

            tk = takeDmg;
        }
        else
        {
            activate = false;
            takeDmg = 0;
          //  Instantia();

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
            //rechargeBar = maxRechargeBar;

            //abilityBar.fillAmount = 0;
            timer += Time.deltaTime;
            if (timer >= maxtimer)
            {
                endShield = false;
                timer = 0;
            }

        }     
    }


    //void Instantia()
    //{
    //     InvokeRepeating("Shoot", 0.0f, cant);
    //}
   
    //void Shoot()
    //{
    //    if (tk > 0)
    //    {
    //        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation); // instancia la bala en la posición y rotación del objeto actual
    //        bullet.transform.LookAt(currentTarget); // apunta la posición de la bala hacia la posición del enemigo
    //        Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //        rb.AddForce(bullet.transform.forward * 30, ForceMode.Impulse);
    //        tk--;
    //    }
    //    else
    //    {
    //        CancelInvoke("Shoot");
    //    }
    //}
    //private void FindNewTarget()
    //{
    //    Collider[] targets = Physics.OverlapSphere(transform.position, range, targetLayer);

    //    float closestDistance = Mathf.Infinity;
    //    Transform closestTarget = null;

    //    foreach (Collider targetCollider in targets)
    //    {
    //        Transform targetTransform = targetCollider.transform;

    //        if (previousTargets.Contains(targetTransform))
    //        {
    //            // Skip targets that have already been selected
    //            continue;
    //        }

    //        float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

    //        if (distanceToTarget < closestDistance)
    //        {
    //            closestDistance = distanceToTarget;
    //            closestTarget = targetTransform;
    //        }
    //    }

    //    currentTarget = closestTarget;
    //    previousTargets.Clear();
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (targetLayer == (targetLayer | (1 << other.gameObject.layer)))
    //    {
    //        if (other.transform == currentTarget)
    //        {
                
    //            return;
    //        }

    //        previousTargets.Add(currentTarget);
    //        currentTarget = null;
    //    }
    //}
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, range);

    //    if (currentTarget != null)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(transform.position, currentTarget.position);
    //        Gizmos.DrawWireSphere(currentTarget.position, 1f);
    //    }
    //}
    public void UpHability()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            maxRechargeBar += 100;
        }
       
    }
}
