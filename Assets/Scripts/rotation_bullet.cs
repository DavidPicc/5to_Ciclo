using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_bullet : MonoBehaviour
{

    public Transform target; // Objeto alrededor del cual se orbitará
    public float orbitSpeed = 2f; // Velocidad de la órbita
    public float orbitRadius = 5f; // Radio de la órbita

    private float angle = 0f; // Ángulo actual de la órbita

    private void Update()
    {
        // Calcula la posición objetivo en la órbita
        Vector3 targetPosition = target.position + (Quaternion.Euler(0f, angle, 0f) * Vector3.forward * orbitRadius);

        // Actualiza la posición del objeto para que orbite alrededor del objetivo
        transform.position = targetPosition;

        // Incrementa el ángulo para la siguiente actualización
        angle += orbitSpeed * Time.deltaTime;
    }
}
