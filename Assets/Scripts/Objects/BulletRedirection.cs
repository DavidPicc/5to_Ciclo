using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRedirection : MonoBehaviour
{
    [SerializeField] bool multiShoot = false;
    [SerializeField] Transform sphereTransform;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            other.transform.position = transform.position;
            other.transform.right = transform.right;
            Vector3 localRight = other.transform.localRotation * Vector3.right;
            other.GetComponent<Rigidbody>().velocity = localRight * other.GetComponent<Rigidbody>().velocity.magnitude;
            if(multiShoot)
            {
                Quaternion newRot = Quaternion.Euler(other.transform.rotation.x, other.transform.rotation.y, other.transform.rotation.z - 15f);
                var bull1 = Instantiate(other.gameObject, sphereTransform.position, newRot);
                bull1.GetComponent<Rigidbody>().velocity = bull1.transform.up * other.GetComponent<Rigidbody>().velocity.magnitude;
                newRot = Quaternion.Euler(other.transform.rotation.x, other.transform.rotation.y, other.transform.rotation.z + 15f);
                var bull2 = Instantiate(other.gameObject, sphereTransform.position, newRot);
                bull2.GetComponent<Rigidbody>().velocity = bull2.transform.up * other.GetComponent<Rigidbody>().velocity.magnitude;
            }
        }
    }
}
