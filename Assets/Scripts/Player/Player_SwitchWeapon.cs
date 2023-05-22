using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SwitchWeapon : MonoBehaviour
{
    Player_Shoot pShoot1;
    Player_Shoot2 pShoot2;
    Player_Shoot3 pShoot3;
    Player_Ability pAbility1;
    Player_Ability2 pAbility2;

    void Start()
    {
        pShoot1 = GetComponent<Player_Shoot>();
        pShoot2 = GetComponent<Player_Shoot2>();
        pShoot3 = GetComponent<Player_Shoot3>();
        pAbility1 = GetComponent<Player_Ability>();
        pAbility2 = GetComponent<Player_Ability2>();


        pShoot1.enabled = true;
        pShoot2.enabled = false;
        pShoot3.enabled = false;

        pAbility1.enabled = true;
        pAbility2.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            pShoot1.enabled = true;
            pShoot2.enabled = false;
            pShoot3.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pShoot1.enabled = false;
            pShoot2.enabled = true;
            pShoot3.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pShoot1.enabled = false;
            pShoot2.enabled = false;
            pShoot3.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            pAbility1.enabled = true;
            pAbility2.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            pAbility1.enabled = false;
            pAbility2.enabled = true;
        }
    }
}
