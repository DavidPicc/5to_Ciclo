using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8_enemy : MonoBehaviour
{
    private float wtime;
    [SerializeField] float timeatack = 3;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawnpoint;

    private void Start()
    {
        wtime = timeatack;
    }
    private void Update()
    {
        if(wtime<=0)
        {
            wtime = timeatack;
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
