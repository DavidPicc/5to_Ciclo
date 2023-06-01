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
        if(Input.GetKeyDown(KeyCode.K))
        {
            Enemy2_Movement[] staticEnemies = GameObject.FindObjectsOfType<Enemy2_Movement>();
            foreach (Enemy2_Movement obj in staticEnemies)
            {
                if (obj.GetComponent<Enemy2_Movement>() != null)
                {
                    // call the specified function in the class
                    obj.GetComponent<Enemy2_Movement>().timeToGo = true;
                    obj.GetComponent<Enemy2_Movement>().locked = false;
                }
            }
        }
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

    void InvokeSituation()
    {
        SituationManager.instance.SpawnSituation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("NextSituation"))
        {
            Enemy2_Movement[] staticEnemies = GameObject.FindObjectsOfType<Enemy2_Movement>();
            foreach (Enemy2_Movement obj in staticEnemies)
            {
                if (obj.GetComponent<Enemy2_Movement>() != null)
                {
                    // call the specified function in the class
                    obj.GetComponent<Enemy2_Movement>().timeToGo = true;
                    obj.GetComponent<Enemy2_Movement>().locked = false;
                }
            }

            //SituationManager.instance.SpawnSituation();
            Destroy(other.gameObject);
            Invoke("InvokeSituation", 0.2f);
        }
    }
}
