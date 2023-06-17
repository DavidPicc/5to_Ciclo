using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{
    public bool enemy;
    public bool elementsofScene;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && enemy == true)
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("ElementsofScene") && elementsofScene == true)
        {
            Destroy(other.gameObject);
        }

    }
}
