using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRedirection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            other.transform.position = transform.position;
            other.transform.right = transform.right;
            Vector3 localRight = other.transform.localRotation * Vector3.right;
            other.GetComponent<Rigidbody>().velocity = localRight * other.GetComponent<Rigidbody>().velocity.magnitude;
        }
    }
}
