using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public static GameScore instance;
    public int currentScore;
    public TextMeshProUGUI scoreText;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        currentScore+= amount;
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
