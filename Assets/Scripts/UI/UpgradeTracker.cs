using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTracker : MonoBehaviour
{
    public static UpgradeTracker instance;

    public Dictionary<string, int> levels;
    public string equippedGun;
    public string equippedSkill;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        } else Destroy(this.gameObject);
    }

    private void Start()
    {
        levels = new Dictionary<string, int>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach(KeyValuePair<string, int> pair in levels)
            {
                Debug.Log(pair.Key + " : " + pair.Value);
            }

            Debug.Log("Equipped Gun: " + equippedGun + ", Equipped Skill: " + equippedSkill);
        }
    }
}   
