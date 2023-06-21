using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet6 : MonoBehaviour
{
    
    public float speed = 10f;  // Velocidad de la bala

    void Update()
    {
        // Mueve la bala hacia la izquierda a la velocidad especificada
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}

