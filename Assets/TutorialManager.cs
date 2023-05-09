using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public GameObject bossDeathTutorial;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        bossDeathTutorial.SetActive(false);
    }
}
