using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]

public class Wave_Values
{
    public enum SpawnType
    {
        allAtOnce,
        oneAfterTheOther
    }
    public SpawnType spawnType;
    [SerializeField] public GameObject[] enemies;

    [SerializeField] public Transform[] spawnPositions;
    [Range(0.0f, 10.0f)]
    [SerializeField] public float offset_X; // Distancia en X entre naves "oneAfterTheOther".
    [Range(0.0f, 10.0f)]
    [SerializeField] public float offset_Y; // Distancia en Y entre naves "allAtOnce".
    [Range(3.0f, 20.0f)]
    [SerializeField] public float time; // Tiempo para la siguiente WAVE.
}
public class Wave_Script : MonoBehaviour
{
    public Wave_Values waveValues;

    [SerializeField] Transform moveParent;

    Transform _camera;

    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
    }

    public void SpawnWave()
    {
        switch (waveValues.spawnType)
        {
            case Wave_Values.SpawnType.allAtOnce:
                Spawn_All();
                break;
            case Wave_Values.SpawnType.oneAfterTheOther:
                Spawn_OneByOne();
                break;
        }
    }

    public void Spawn_All()
    {
        float offset = 0;
        if(waveValues.enemies.Length > 1)
        {
            offset -= waveValues.offset_Y / 2;
        }

        for (int i = 0; i < waveValues.enemies.Length; i++)
        {
            Debug.Log(offset);
            Vector3 offsetVector = new Vector3(0, offset, 0);
            var enemy = Instantiate(waveValues.enemies[i], transform.position + offsetVector, waveValues.enemies[i].transform.rotation, moveParent);
            offset += waveValues.offset_Y / (waveValues.enemies.Length-1);
        }
    }

    public void Spawn_OneByOne()
    {
        for (int i = 0; i < waveValues.enemies.Length; i++)
        {
            float randomY = Random.Range(-5f, 5f);
            Vector3 offsetVector = new Vector3(waveValues.offset_X * i, randomY,0);
            var enemy = Instantiate(waveValues.enemies[i], transform.position + offsetVector, waveValues.enemies[i].transform.rotation, moveParent);
        }
    }
}
