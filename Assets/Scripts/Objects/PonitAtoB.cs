using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonitAtoB : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2.0f;

    private Vector3 objetivo;

    void Start()
    {
        objetivo = puntoA.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);

        if (transform.position == puntoA.position)
        {
            objetivo = puntoB.position;
        }
        else if (transform.position == puntoB.position)
        {
            objetivo = puntoA.position;
        }
    }
}
