using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightIncrease : MonoBehaviour
{
    public Rigidbody rb;
    public float weightIncrease = 2.0f;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        InvokeRepeating("IncreaseWeight", 1.0f, 1.0f);
    }
    void IncreaseWeight()
    {
        rb.mass += weightIncrease;
    }
}
