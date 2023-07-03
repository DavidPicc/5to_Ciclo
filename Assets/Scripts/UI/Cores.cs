using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cores : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cores"))
        {   
            Destroy(other.gameObject);
            score++;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = " " + score.ToString();
    }
}
