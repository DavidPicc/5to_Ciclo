using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hability : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] float rechargeBar;
    [SerializeField] float maxrechargeBar;
    [SerializeField] Shield shield;


    public bool endShield;
    public bool Activate;
    public float timer;
    public float Maxtimer;
    void Start()
    {
        rechargeBar = maxrechargeBar;
    }

    // Update is called once per frame
    void Update()
    {
        ActivateShield();
    }
    public  void ActivateShield()
    {
        if (Input.GetKey(KeyCode.C))
        {
            Activate = true;
        }
        else
        {
            Activate = false;
        }

        if (Activate)
        {
            rechargeBar--;

            if (!endShield)
            {
                if (rechargeBar >= 0)
                {
                    shieldObj.SetActive(true);
                }
                else
                {
                    endShield = true;
                    shieldObj.SetActive(false);
                }
            }
        }
        else
        {
            shieldObj.SetActive(false);
        }
        if (endShield)
        {
            rechargeBar = maxrechargeBar;

            
            timer += Time.deltaTime;
            if (timer >= Maxtimer)
            {
                endShield = false;
                timer = 0;
            }

        }
       
        
    }


    public void ReceiveDamage()
    {

    }



}
