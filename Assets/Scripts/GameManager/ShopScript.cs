using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gearsScoreShop, coresScoreShop;
    [SerializeField] TextMeshProUGUI gun1CostText;
    public static int gun1Cost = 0;
    [SerializeField] int gun1BaseCost;
    [SerializeField] Button gun1Button;

    void Start()
    {
        gearsScoreShop.text = GameScore.instance.gearScore.ToString();
        coresScoreShop.text = GameScore.instance.coreScore.ToString();

        gun1Cost += gun1BaseCost;
        gun1CostText.text = gun1Cost.ToString();
    }

    void Update()
    {
        
    }

    public void LevelUpGun1()
    {
        if(GameScore.instance.gearScore > gun1Cost)
        {
            //FindObjectOfType<Player_Shoot>().Upgrades();
            GameScore.instance.RemoveGears(gun1Cost);
            gearsScoreShop.text = GameScore.instance.gearScore.ToString();
            UpdateCost(gun1CostText, gun1Cost+200);
        }
        if (FindObjectOfType<TutorialManager>() != null)
        {
            GameManager.instance.CloseShop();
        }
    }

    void UpdateCost(TextMeshProUGUI costText, int costAmount)
    {
        costText.text = costAmount.ToString();
    }
}
