using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public static GameScore instance;
    GameObject player;
    public int gearScore, coreScore;

    [SerializeField] public int playerLevel = 0;
    [SerializeField] public int scoreToLevelUp;
    //public int invisibleScore;

    private void Awake()
    {
        instance = this;

        AddGears(200);
        AddCores(0);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //AddLevel();
    }

    void Update()
    {
        //if(scoreToNextLevel >= scoreToLevelUp)
        //{
        //    AddLevel();
        //}
    }

    public void AddGears(int amount)
    {
        gearScore = gearScore + amount;
        //invisibleScore = invisibleScore + amount;
        CanvasUI.instance.gearsText.text = gearScore.ToString();
    }

    public void RemoveGears(int amount)
    {
        gearScore = gearScore - amount;
        CanvasUI.instance.gearsText.text = gearScore.ToString();
    }

    public void AddCores(int amount)
    {
        coreScore = coreScore + amount;
        CanvasUI.instance.coresText.text = coreScore.ToString();
    }

    //public void AddLevel()
    //{
    //    playerLevel++;
    //    CanvasUI.instance.playerLevelText.text = "Player level: " + playerLevel.ToString();
    //    scoreToLevelUp += 30; // Para el siguiente nivel tienes que hacer 30 puntos más que en el anterior.
    //}
}
