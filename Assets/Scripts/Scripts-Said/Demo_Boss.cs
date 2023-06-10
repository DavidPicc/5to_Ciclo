using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Boss : MonoBehaviour
{
    Transform player;
    Transform _camera;
    [SerializeField] float invulnerabilityTime;
    float timer;
    [SerializeField] float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] float halfthealt;
    [SerializeField] float moveSpeed = 2f;
    public Transform[] movePoints;
    private int currentMovePointIndex = 0;
    public bool canBeDamaged => transform.position.x - _camera.position.x <= 12f && timer >= invulnerabilityTime;

    public void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        timer = invulnerabilityTime;
    }

    public void Update()
    {
        if (timer <= 0)
        {
            timer = invulnerabilityTime;
        }

        if (!canBeDamaged)
        {
            timer -= Time.deltaTime;
        }

        if (currentHealth <= halfthealt)
        {
            MoveToNextPoint();
        }
    }

    private void MoveToNextPoint()
    {
        if (currentMovePointIndex >= movePoints.Length)
        {
            currentMovePointIndex = 0;
        }

        Transform nextMovePoint = movePoints[currentMovePointIndex];

        transform.position = Vector3.MoveTowards(transform.position, nextMovePoint.position, moveSpeed * Time.deltaTime);

        if (transform.position == nextMovePoint.position)
        {
            currentMovePointIndex++;
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > damage)
        {
            currentHealth -= damage;
        }

        else
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            if (canBeDamaged)
            {
                TakeDamage(1);
            }
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
