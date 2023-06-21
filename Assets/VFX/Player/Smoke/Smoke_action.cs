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
       Instantiate (vfx, Firepoint.transform.position, Quaternion.identity);
        
    }
}
