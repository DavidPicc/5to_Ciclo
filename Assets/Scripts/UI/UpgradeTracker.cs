using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTracker : MonoBehaviour
{
    public static UpgradeTracker instance;

    public Dictionary<string, int> levels;

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

        levels.Add("gun1", 1);
        levels.Add("gun2", 1);
        levels.Add("gun3", 1);
        levels.Add("skill1", 1);
        levels.Add("skill2", 1);
    }
}
