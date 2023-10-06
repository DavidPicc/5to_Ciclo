using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_action : MonoBehaviour
{
    public GameObject Firepoint;
    public GameObject vfx;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            var smoke = Instantiate(vfx, Firepoint.transform.position, Quaternion.identity);
            Destroy(smoke.gameObject, 0.05f);
        }
    }
}
