using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy4_movement : MonoBehaviour
{
    public GameObject enemigo4;
    public Transform punto1;
    public Transform punto2;
    public float Velocidad;
    private Vector3 moverhacia;

    void Start()
    {
        moverhacia = punto2.position;
    }

    void Update()
    {
        enemigo4.transform.position = Vector3.MoveTowards(enemigo4.transform.position, moverhacia, Velocidad * Time.deltaTime);
            if(enemigo4.transform.position== punto2.position)
        {
            moverhacia = punto1.position;
        }
        if (enemigo4.transform.position == punto1.position)
        {
            moverhacia = punto2.position;
        }
    }
}

