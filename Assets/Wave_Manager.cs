using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave_Manager : MonoBehaviour
{
    //public GameObject[] waveScripts;
    //public List<Wave_Script> wave_Scripts = new List<Wave_Script>();
    public Wave_Values[] waveValues;
    //public float restTime;
    float timer;
    int wave = -1;
    void Start()
    {
        timer = FindObjectOfType<Wave_Script>().waveValues.time;        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            NextWave();
            timer = FindObjectOfType<Wave_Script>().waveValues.time;
        }
    }

    void NextWave()
    {
        if (wave < waveValues.Length)
        {
            wave++;
            FindObjectOfType<Wave_Script>().waveValues = waveValues[wave];
            FindObjectOfType<Wave_Script>().SpawnWave();
        }
        else
            Debug.Log("You finished all the waves!");
    }
}
