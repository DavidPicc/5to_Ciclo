using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemi8_buller : MonoBehaviour
{
    public float speed = 2;
    public float lifetime = 1;
    public bool left;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    { 
        if (left)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
