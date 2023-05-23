using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet2 : MonoBehaviour
{
    public float explosionRadius = 3f; // The radius of the explosion
    public float explosionDelay = 0.5f; // The delay before the bullet explodes
    public float explosionDamage = 10f;
    public GameObject explosionRadVisual;

    void Start()
    {
        explosionRadVisual.SetActive(false);
    }

    public void Explosion()
    {
        Debug.DrawRay(transform.position, Vector3.up, Color.red, explosionRadius);
        explosionRadVisual.SetActive(true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponentInParent<Enemy_Health>() != null)
            {
                collider.GetComponentInParent<Enemy_Health>().TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Obstacle") || other.CompareTag("EnemyBullet"))
        {
            Explosion();
        }
    }
}
