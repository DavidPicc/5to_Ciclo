using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager_RE : MonoBehaviour
{
    public static SituationManager_RE instance;
    public GameObject[] situations;
    Transform _camera;
    public int waveIndex = 0;
    public int currentWave = 0;
    //public int wave;
    public float situationOffset = 27f;
    public Vector3 spawnPosition;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //_camera = FindObjectOfType<StageMovement>().transform;
        //spawnPosition = _camera.position + new Vector3(situationOffset, 0, 0);
        //waveIndex += 1;
        //currentWave = waveIndex - 1;
        ActivateSituation();
    }
    public void ActivateSituation()
    {
        if(waveIndex < situations.Length - 1)
        {
            situations[waveIndex].SetActive(true);
            waveIndex += 1;
        }
        currentWave += 1;
    }
}
