using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public static GameScore instance;
    GameObject player;
    public int totalScore;
    [SerializeField] public int playerLevel = 0;
    [SerializeField] public int scoreToLevelUp;
    public int invisibleScore;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AddLevel();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            AddLevel();
        }

        //if(scoreToNextLevel >= scoreToLevelUp)
        //{
        //    AddLevel();
        //}
    }

    public void AddScore(int amount)
    {
        totalScore = totalScore + amount;
        invisibleScore = invisibleScore + amount;
        if (invisibleScore >= scoreToLevelUp)
        {
            AddLevel();
        }
        CanvasUI.instance.scoreText.text = "Score: " + totalScore.ToString();
    }

    public void AddLevel()
    {
        playerLevel++;
        CanvasUI.instance.playerLevelText.text = "Player level: " + playerLevel.ToString();
        invisibleScore = 0;
        scoreToLevelUp += 30; // Para el siguiente nivel tienes que hacer 30 puntos más que en el anterior.
        if(playerLevel == 1)
        {
            player.GetComponent<Player_Manager>().maxCannons = 1;

        }
        else if(playerLevel == 2)
        {
            player.GetComponent<Player_Manager>().maxCannons = 3;
            player.GetComponent<Player_Shoot>().fireRate += 0.1f;
        }
        else if (playerLevel == 3)
        {
            player.GetComponent<Player_Manager>().maxCannons = 5;
            player.GetComponent<Player_Shoot>().fireRate += 0.05f;
        }
    }
}
