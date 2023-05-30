using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_bullet : MonoBehaviour
{

    public Transform target; // Objeto alrededor del cual se orbitar�
    public float orbitSpeed = 2f; // Velocidad de la �rbita
    public float orbitRadius = 5f; // Radio de la �rbita

    private float angle = 0f; // �ngulo actual de la �rbita

    private void Update()
    {
        // Calcula la posici�n objetivo en la �rbita
        Vector3 targetPosition = target.position + (Quaternion.Euler(0f, angle, 0f) * Vector3.forward * orbitRadius);

        // Actualiza la posici�n del objeto para que orbite alrededor del objetivo
        transform.position = targetPosition;

        // Incrementa el �ngulo para la siguiente actualizaci�n
        angle += orbitSpeed * Time.deltaTime;
    }
}
