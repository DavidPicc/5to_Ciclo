using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wave_Script : MonoBehaviour
{
    public enum SpawnType
    {
        allAtOnce,
        oneAfterTheOther
    }
    public SpawnType spawnType;
    [SerializeField] GameObject[] enemies;

    [SerializeField] Transform[] spawnPositions;
    [Range(0.0f, 10.0f)]
    [SerializeField] float offset_X;
    [Range(0.0f, 10.0f)]
    [SerializeField] float offset_Y;

    int lastRandom = 0;

    [SerializeField] Transform moveParent;

    Transform _camera;

    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;

        switch(spawnType)
        {
            case SpawnType.allAtOnce:
                Spawn_All();
                break;
            case SpawnType.oneAfterTheOther:
                Spawn_OneByOne();
                break;
        }
    }

    //public void Spawn_All()
    //{
    //    for (int i = 0; i < enemyQuantity; i++)
    //    {
    //        int random = Random.Range(0, spawnPositions.Length);
    //        if (lastRandom != random)
    //        {
    //            Vector3 spawnPos = spawnPositions[random].position + new Vector3(offset_X * i, 0, 0);
    //            var enemy = Instantiate(enemies[0], spawnPos, enemies[0].transform.rotation, moveParent);
    //        }
    //        else i--;
    //        lastRandom = random;
    //    }
    //}

    public void Spawn_All()
    {
        float offset = 0;
        if(enemies.Length > 1)
        {
            offset -= offset_Y / 2;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log(offset);
            Vector3 offsetVector = new Vector3(0, offset, 0);
            var enemy = Instantiate(enemies[i], transform.position + offsetVector, enemies[i].transform.rotation, moveParent);
            offset += offset_Y / (enemies.Length-1);
        }
    }

    public void Spawn_OneByOne()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            float randomY = Random.Range(-5f, 5f);
            Vector3 offsetVector = new Vector3(offset_X * i, randomY,0);
            var enemy = Instantiate(enemies[i], transform.position + offsetVector, enemies[i].transform.rotation, moveParent);
        }
    }
}
