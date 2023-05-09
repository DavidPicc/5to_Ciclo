using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public int maxCannons;
    [SerializeField] public int[] importantLevels;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CheckLevel()
    {
        if(GameScore.instance.gearScore >= importantLevels[0])
        {
            maxCannons = 1;
        }
        else if(GameScore.instance.gearScore >= importantLevels[1])
        {
            maxCannons = 3;
        }
        else if(GameScore.instance.gearScore >= importantLevels[2])
        {
            maxCannons = 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("NextSituation"))
        {
            SituationManager.instance.SpawnSituation();
            Destroy(other.gameObject);
        }
    }
}
