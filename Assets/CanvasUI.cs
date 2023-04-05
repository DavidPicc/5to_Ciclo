using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    public static CanvasUI instance;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI playerLevelText;
    [SerializeField] public TextMeshProUGUI waveText;

    private void Awake()
    {
        instance = this;
    }
}
