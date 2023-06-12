using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float RotationSpeed;
    [SerializeField] float MovementSpeed;
    [SerializeField] float time;
    public Player_Health playerHealth;
    public GameObject Advice;
    public bool rotation = true;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool moveUp = false;
    public bool moveDown = false;
    public bool Damage = false;
    public bool advice = false;



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

        if (moveRight == true)
        {
            transform.Translate(Vector3.right * MovementSpeed * Time.deltaTime);
        }

        if (moveUp == true)
        {
            transform.Translate(Vector3.up * MovementSpeed * Time.deltaTime);
        }

        if (moveDown == true)
        {
            transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Damage == true && other.CompareTag("Player"))
        {
            playerHealth.TakeDamage(1);
        }
    }

    IEnumerator DeactivateObjectDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Advice.SetActive(false);
    }

    public void ActivateDeactivation()
    {
        if (advice == true && Advice != null)
        {
            StartCoroutine(DeactivateObjectDelayed(time));
        }
    }
}
