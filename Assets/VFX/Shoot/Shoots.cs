using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoots : MonoBehaviour
{
    public GameObject Firepoint;
    public List<GameObject> vfx = new List < GameObject > ();
    private GameObject efecttspawn; 
    void Start()
    {
        efecttspawn = vfx[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shoot();
        }
        
    }
    public void shoot()
    {
        GameObject vfx;
        if (Firepoint != null)
        {
            vfx = Instantiate(efecttspawn, Firepoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("lag");
        }
    }
}
