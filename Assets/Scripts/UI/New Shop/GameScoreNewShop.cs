using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScoreNewShop : MonoBehaviour
{
    public bool debug;

    public static GameScoreNewShop instance;

    public int gears, cores;

    public TextMeshProUGUI gearsUI, coresUI;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(GameScore.instance != null && !debug)
        {
            gears = GameScore.instance.gearScore;
            cores = GameScore.instance.coreScore;
        }

        if(gearsUI != null) gearsUI.text = gears.ToString();
        if(coresUI != null) coresUI.text = cores.ToString();
    }

    public void Spend(int gears, int cores)
    {
        if (this.gears < gears || this.cores < cores) return;

        if (GameScore.instance != null && !debug) 
        {
            GameScore.instance.RemoveGears(gears);
            GameScore.instance.RemoveCores(cores);
        }
        else
        {
            this.gears -= gears;
            this.cores -= cores;
        }
    }
}
