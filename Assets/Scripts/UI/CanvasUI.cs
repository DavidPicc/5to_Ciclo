using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasUI : MonoBehaviour
{
    public static CanvasUI instance;
    [SerializeField] public TextMeshProUGUI gearsText;
    [SerializeField] public TextMeshProUGUI coresText;
    [SerializeField] public TextMeshProUGUI playerLevelText;
    [SerializeField] public TextMeshProUGUI waveText;
    [SerializeField] public Image playerAbilityBar;

    private void Awake()
    {
        instance = this;
    }
}
