using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8_Bullet : MonoBehaviour
{
    public float explosionRadius = 3f; // The radius of the explosion
    public float explosionForce = 100f; // The force of the explosion
    public float explosionDamage = 10f; // The damage of the explosion
    public float explosionDelay = 2f; // The delay before the bullet explodes
    public float bulletMaxHealth;
    float health;
    [SerializeField] public float invulnerabilityTime;
    float timer;
    bool canBeDamaged => timer >= invulnerabilityTime;

    void Start()
    {
        // Start the explosion delay coroutine
        StartCoroutine(ExplosionDelay());

        timer = invulnerabilityTime;
        health = bulletMaxHealth;
        gameObject.transform.parent = null;
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(GetComponent<Rigidbody>().velocity, Vector3.zero, 35 * Time.deltaTime);
    }

    private IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(explosionDelay);

        // Check for objects within the explosion radius
        Explosion();
    }

    public void Explosion()
    {
        Debug.DrawRay(transform.position, Vector3.up, Color.red, explosionRadius);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponentInParent<Player_Health>() != null)
            {
                collider.GetComponentInParent<Player_Health>().TakeDamage(1);
            }
            else if (collider.GetComponentInParent<Enemy_Health>() != null)
            {
                collider.GetComponentInParent<Enemy_Health>().TakeDamage(10);
            }

            Debug.Log(collider.name + " has been damaged!");
        }
        Debug.Log(transform.position);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (health > damage)
        {
            health -= damage;
        }
        else
        {
            Explosion();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            if(canBeDamaged)
            {
                TakeDamage(1);
            }

        }
    }

    //private void OnDestroy()
    //{
    //    // If the bullet is destroyed before the explosion, cancel the explosion delay coroutine
    //    StopCoroutine(ExplosionDelay());
    //}
}
