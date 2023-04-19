using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Object : MonoBehaviour
{
    public GameObject explosion;
    [SerializeField] public float coldown;
    float timer;
    bool exploded = false;
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
            exploded = true;
            explosion.SetActive(true);
           
        }
    }

   
}
