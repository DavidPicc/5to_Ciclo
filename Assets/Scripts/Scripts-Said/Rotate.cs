using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 10;
    [SerializeField] float MovementSpeed = 5;
    public bool rotation = true;
    public bool moveLeft = false;

    void Update()
    {
        if (rotation == true)
        {
            transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
        }

        if (moveLeft == true)
        {
            transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
        }
    }
}
