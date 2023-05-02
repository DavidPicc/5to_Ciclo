using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{
    public static SituationManager instance;
    //public GameObject[] situations;
    public SituationScript[] situations;
    int dialogueIndex = 0;
    Transform _camera;
    int index = 0;
    public int wave = 0;
    public float situationOffset = 27f;
    public float timeToSpawn;
    float timer;
    public Vector3 spawnPosition;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        //timer = timeToSpawn;
        spawnPosition = _camera.position + new Vector3(0, 0, 0);

        SpawnSituation();
    }
    void Update()
    {
        //timer -= Time.deltaTime;
        //if (timer <= 0)
        //{
        //    SpawnSituation();
        //}


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanvasUI.instance.dialogueObject.activeSelf)
            {
                if(TypeWriterText.instance.currentText != TypeWriterText.instance.fullText)
                {
                    FullText();
                }
                else
                {
                    if (dialogueIndex < situations[index].dialogue.Length - 1)
                    {
                        dialogueIndex++;
                    }
                    else
                    {
                        CanvasUI.instance.dialogueObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void TextAppear()
    {
        TypeWriterText.instance.fullText = situations[index].dialogue[dialogueIndex];
        StartCoroutine(TypeWriterText.instance.ShowText());
    }

    public void FullText()
    {
        //CanvasUI.instance.dialogueText.text = TypeWriterText.instance.fullText;
        StopCoroutine(TypeWriterText.instance.ShowText());
        TypeWriterText.instance.currentText = TypeWriterText.instance.fullText;
        //TypeWriterText.instance.running = false;

    }

    public void SpawnSituation()
    {
        var sit = Instantiate(situations[index].situationPrefab, spawnPosition, Quaternion.identity);
        if (situations[index].dialogue.Length > 0)
        {
            CanvasUI.instance.dialogueObject.SetActive(true);
            //CanvasUI.instance.dialogueText.text = situations[index].dialogue[dialogueIndex];
            TextAppear();
        }
        //lastSituationX = sit.gameObject.transform.position.x;
        spawnPosition += new Vector3(situationOffset, 0, 0);
        timer = timeToSpawn;
        wave++;
        if (index < situations.Length - 1)
        {
            index++;
        }
        //else
        //{
        //    index = 0;
        //}
    }
}
