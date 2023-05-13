using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Object : MonoBehaviour
{
    public GameObject explosion;
    [SerializeField] public float coldown;
    float timer;
    bool exploded = false;
    [SerializeField] GameObject vfxexplosion;

    void Start()
    {

    }

    void Update()
    {
        if (exploded)
        {
            timer += Time.deltaTime;
            if(timer >= coldown)
            {
                Destroy(gameObject);
            }
        }
   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet")) 
        {
            //Instantiate(vfxexplosion, transform.position, transform.rotation);
            exploded = true;
            explosion.SetActive(true);
           
        }
    }

   
}
