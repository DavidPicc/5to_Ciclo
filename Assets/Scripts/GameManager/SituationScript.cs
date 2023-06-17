using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SituationScript
{

    public GameObject situationPrefab;
    [SerializeField, TextArea(3, 8)]
    public string[] dialogue;
    public bool isShop;
    public bool isCheckpoint;
    public bool isBoss;
    public UnityEvent methodEvent;
}
