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
    [SerializeField] public GameObject dialogueObject;
    [SerializeField] public Image dialoguePortrait;
    [SerializeField] public TextMeshProUGUI dialogueName;
    [SerializeField] public TextMeshProUGUI dialogueText;
    [SerializeField] public GameObject shopObject;

    private void Awake()
    {
        instance = this;
    }
}
