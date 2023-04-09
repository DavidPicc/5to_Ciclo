using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy8_movement : MonoBehaviour
{
    public GameObject enemigo8;
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
        enemigo8.transform.position = Vector3.MoveTowards(enemigo8.transform.position, moverhacia, Velocidad * Time.deltaTime);
            if(enemigo8.transform.position== punto2.position)
        {
            moverhacia = punto1.position;
        }
        if (enemigo8.transform.position == punto1.position)
        {
            moverhacia = punto2.position;
        }
    }
}

