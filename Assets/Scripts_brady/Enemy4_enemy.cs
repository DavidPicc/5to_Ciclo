using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4_enemy : MonoBehaviour
{
    private float wtime;
    public float tiempoataque = 3;
    public GameObject bullet;
    public Transform spawnpoint;

    private void Start()
    {
        wtime = tiempoataque;
    }
    private void Update()
    {
        if(wtime<=0)
        {
            wtime = tiempoataque;
            Invoke("luchebullet", 0.5f);
        }
        else
        {
            wtime -= Time.deltaTime;
        }
    }
    public void luchebullet()
    {
        GameObject newbullet;
        newbullet = Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
    }
}
