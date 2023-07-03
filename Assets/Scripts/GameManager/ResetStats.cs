using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameScore.instance != null)
        {
            Destroy(GameScore.instance.gameObject);
            GameScore.instance = null;
            Destroy(UpgradeTracker.instance.gameObject);
            UpgradeTracker.instance = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
