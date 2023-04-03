using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wave_Script : MonoBehaviour
{
    public GameObject[] randomEnemies;
    public GameObject[] enemies;
    public int enemyQuantity;

    public Transform[] spawnPositions;
    public float offset_X;
    public float offset;

    int lastRandom = 0;

    void Start()
    {
        for(int i = 0; i < enemyQuantity; i++)
        {
            int random = Random.Range(0, spawnPositions.Length);
            if (lastRandom != random)
            {
                Vector3 spawnPos = spawnPositions[random].position + new Vector3(offset_X * i, 0, 0);
                var enemy = Instantiate(enemies[0], spawnPos, enemies[0].transform.rotation);
            }
            else i--;
            lastRandom = random;
        }
    }

    void Update()
    {
        
    }
}
