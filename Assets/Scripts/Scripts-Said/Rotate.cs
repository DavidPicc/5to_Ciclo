using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float RotationSpeed;
    [SerializeField] float MovementSpeed;
    public Player_Health playerHealth;
    public bool rotation = true;
    public bool moveLeft = false;
    public bool Damage = false;

    private void Start()
    {
        playerHealth = FindAnyObjectByType<Player_Health>();
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (Damage == true && other.CompareTag("Player"))
        {
            playerHealth.TakeDamage(1);
        }
    }
}
