using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{
    public static SituationManager instance;
    //public GameObject[] situations;
    public SituationScript[] situations;
    Transform _camera;
    int index = 0;
    public int wave = 0;
    public float situationOffset = 27f;
    public float timeToSpawn;
    float timer;
    public Vector3 spawnPosition;

    public int bossWave;

    public float textTimerDisappear;
    float textTimer;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        //timer = timeToSpawn;
        spawnPosition = _camera.position + new Vector3(0, 0, 0);
        //wave = CheckPointScript.savedWave;
        index = wave;

        SpawnSituation();
    }
    void Update()
    {

    }

    void SpecialSituationsTutorial()
    {
        if(wave == situations.Length)
        {
            TutorialManager.instance.VulnerableFriendsAndYou();
        }
        else if (wave == bossWave)
        {
            TutorialManager.instance.ActivateBoss();
        }
    }

    void SpecialSituations()
    {
        if (situations[index].isShop)
        {
            GameManager.instance.OpenShop();
        }
        if (situations[index].isCheckpoint)
        {
            CheckPointScript.instance.UpdateCheckpoints();
        }
    }

    public void SpawnSituation()
    {
        var sit = Instantiate(situations[index].situationPrefab, spawnPosition, Quaternion.identity);
        if (situations[index].dialogue.Length > 0)
        {
            DialogueScript.instance.SetDialogue(situations[index].dialogue);
        }
        spawnPosition += new Vector3(situationOffset, 0, 0);
        timer = timeToSpawn;
        wave++;

        SpecialSituations();
        if(FindObjectOfType<TutorialManager>() != null)
        {
            SpecialSituationsTutorial();
        }


        if (index < situations.Length - 1)
        {
            index++;
        }
    }
}
