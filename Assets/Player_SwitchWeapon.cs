using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SwitchWeapon : MonoBehaviour
{
    Player_Shoot pShoot1;
    Player_Shoot2 pShoot2;
    void Start()
    {
        pShoot1 = GetComponent<Player_Shoot>();
        pShoot2 = GetComponent<Player_Shoot2>();

        pShoot1.enabled = true;
        pShoot2.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            pShoot1.enabled = true;
            pShoot2.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pShoot1.enabled = false;
            pShoot2.enabled = true;
        }
    }
}
