using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    void Start()
    {
        if(GameScore.instance != null)
        {
            Destroy(GameScore.instance.gameObject);
            GameScore.instance = null;
        }
        if(UpgradeTracker.instance != null)
        {
            Destroy(UpgradeTracker.instance.gameObject);
            UpgradeTracker.instance = null;
        }
    }
}
