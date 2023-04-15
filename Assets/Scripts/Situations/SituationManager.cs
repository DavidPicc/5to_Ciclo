using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{
    public GameObject[] situations;
    Transform _camera;
    int index = 0;
    public int wave = 0;
    public float situationOffset = 27f;
    float lastSituationX = 0;
    public float timeToSpawn;
    float timer;
    public Vector3 spawnPosition;
    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        //timer = timeToSpawn;
        spawnPosition = _camera.position + new Vector3(situationOffset, 0, 0);

        SpawnSituation();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnSituation();
        }
    }

    public void SpawnSituation()
    {
        var sit = Instantiate(situations[index], spawnPosition, Quaternion.identity);
        //lastSituationX = sit.gameObject.transform.position.x;
        spawnPosition += new Vector3(situationOffset, 0, 0);
        timer = timeToSpawn;
        wave++;
        if (index < situations.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
    }
}
