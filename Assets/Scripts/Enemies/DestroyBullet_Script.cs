using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet_Script : MonoBehaviour
{
    public float destroyDelay = 10f;
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
