using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager_RE : MonoBehaviour
{
    public static SituationManager_RE instance;
    public SituationScript[] situations;
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
        _camera = FindObjectOfType<StageMovement>().transform;
        spawnPosition = _camera.position + new Vector3(situationOffset, 0, 0);
        waveIndex += 1;
        currentWave = waveIndex - 1;
        SpawnSituation();
    }
    public void SpawnSituation()
    {
        if(waveIndex < situations.Length - 1)
        {
            var sit = Instantiate(situations[waveIndex].situationPrefab, spawnPosition, Quaternion.identity);
            spawnPosition += new Vector3(situationOffset, 0, 0);
            waveIndex += 1;
        }
        if(currentWave - 1 >= 0)
        {
            if (situations[currentWave - 1].dialogue.Length > 0)
            {
                DialogueScript.instance.SetDialogue(situations[currentWave - 1].dialogue);
            }
            if (situations[currentWave - 1].methodEvent != null)
            {
                situations[currentWave - 1].methodEvent.Invoke();
            }
        }
        currentWave += 1;

        // Para que los enemigos que se quedan quietos en la pantalla se muevan una vez toque su turno.
        Enemy2_Movement[] enemies = GameObject.FindObjectsOfType<Enemy2_Movement>();
        foreach(Enemy2_Movement enemy2 in enemies)
        {
            if(enemy2.locked)
            {
                enemy2.locked = false;
                enemy2.timeToGo = true;
            }
        }
    }
}
