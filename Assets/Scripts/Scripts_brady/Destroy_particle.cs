using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_particle : MonoBehaviour
{
   [SerializeField] GameObject vfx;
    // Start is called before the first frame update
    void Start()
    {
      Destroy(vfx, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
