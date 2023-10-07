using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{
    public bool enemy;
    public bool elementsofScene;
    public bool bullets;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && enemy)
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("ElementsofScene") && elementsofScene)
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("PlayerBullet") && bullets)
        {
            Destroy(other.gameObject);
        }
    }
}
