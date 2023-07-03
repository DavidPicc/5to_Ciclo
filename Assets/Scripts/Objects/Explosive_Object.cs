using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Object : MonoBehaviour
{
    public GameObject explosion;
    [SerializeField] public float coldown;
    float timer;
    bool exploded = false;
    [SerializeField] GameObject vfxexplosion;
    [SerializeField] float proximityDistance = 2f;
    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (exploded)
        {
            timer += Time.deltaTime;
            if(timer >= coldown)
            {
                Destroy(gameObject);
            }
        }

        if (!exploded && playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= proximityDistance)
        {
            exploded = true;
            explosion.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet")) 
        {
            //Instantiate(vfxexplosion, transform.position, transform.rotation);
            exploded = true;
            explosion.SetActive(true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, proximityDistance);
    }
}
