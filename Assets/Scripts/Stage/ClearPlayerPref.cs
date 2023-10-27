using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPref : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        if(UpgradeTrackerNewShop.instance != null)
        {
            Destroy(UpgradeTrackerNewShop.instance.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
