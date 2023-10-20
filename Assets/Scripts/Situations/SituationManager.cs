using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{
    public static SituationManager instance;
    public SituationScript[] situations;
    public GameObject[] situationsObject;
    Transform _camera;
    public int waveIndex = 0;
    public int currentWave = 0;
    //float firstOffset = 5f;
    public float situationOffset = 27f;
    public Vector3 spawnPosition;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        // Si la SAVEDWAVED es cualquiera menos 0, te manda directo a la acción. Si es CERO, signfica que recién estás empezando el nivel.
        if(CheckPointScript.savedWave != 0)
        {
            waveIndex = CheckPointScript.savedWave;
            if(waveIndex -2 >= 0)
                SpawnSituationPast(waveIndex - 2);
            if (waveIndex - 1 >= 0)
                SpawnSituationPast(waveIndex - 1);
            foreach(GameObject tag in GameObject.FindGameObjectsWithTag("NextSituation"))
            {
                Destroy(tag);
            }
        }
        SpawnSituation();
        //currentWave = waveIndex - 1;
        Debug.Log("Current wave: " + (waveIndex-1));
    }

    public void ActivateSituation(int wave)
    {
        situationsObject[wave].SetActive(true);
    }

    void SpawnSituationPast(int index)
    {
        ActivateSituation(index);
        //Destroy(GameObject.FindWithTag("NextSituation"));
    }
    public void SpawnSituation()
    {
        if (waveIndex <= situations.Length - 1)
        {
            ActivateSituation(waveIndex);
        }
        if(waveIndex- 1 >= 0)
        {
            if (situations[waveIndex - 1].dialogue.Length > 0)
            {
                DialogueScript.instance.SetDialogue(situations[waveIndex - 1].dialogue, GetComponent<AudioSource>());
            }
            if (situations[waveIndex - 1].methodEvent != null)
            {
                situations[waveIndex - 1].methodEvent.Invoke();
            }
        }
       
        waveIndex += 1;
        currentWave += 1;

        // Para que los enemigos que se quedan quietos en la pantalla se muevan una vez toque su turno.
        Enemy2_Movement[] enemies = GameObject.FindObjectsOfType<Enemy2_Movement>();
        foreach (Enemy2_Movement enemy2 in enemies)
        {
            if (enemy2.locked)
            {
                enemy2.locked = false;
                enemy2.timeToGo = true;
            }
        }
    }
}
