using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy8_movement : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    [SerializeField] float Velocidad;
    private Vector3 movemenpositión;

    void Start()
    {
        movemenpositión = point2.position;
    }

    void Update()
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, movemenpositión, Velocidad * Time.deltaTime);
            if(enemy.transform.position== point2.position)
        {
            movemenpositión = point1.position;
        }
        if (enemy.transform.position == point1.position)
        {
            movemenpositión = point2.position;
        }
    }
}

