using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet2 : MonoBehaviour
{
    public float explosionRadius = 3f; // The radius of the explosion
    public float explosionDelay = 0.5f; // The delay before the bullet explodes
    public float explosionDamage = 10f;
    public GameObject explosionVFX;

    void Start()
    {
        explosionVFX.SetActive(false);
    }
    public void Explosion()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.DrawRay(transform.position, Vector3.up, Color.red, explosionRadius);
        ShowExplosion();
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponentInParent<Enemy_Health>() != null)
            {
                collider.GetComponentInParent<Enemy_Health>().TakeDamage(explosionDamage);
            }
            if (collider.GetComponentInParent<objet_destrur>() != null)
            {
                collider.GetComponentInParent<objet_destrur>().TakeDamage(explosionDamage*20);
                Debug.Log("Explosion range: " + collider.gameObject.name);
            }
        }
        Destroy(gameObject, 0.1f);
    }
    public void DelayExplosion(float time)
    {
        Invoke("Explosion", time);
    }

    void ShowExplosion()
    {
        explosionVFX.transform.parent = null;
        explosionVFX.SetActive(true);
        Destroy(explosionVFX, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Obstacle") || other.CompareTag("EnemyBullet") || other.CompareTag("destructible"))
        {
            Explosion();
        }
    }
}
