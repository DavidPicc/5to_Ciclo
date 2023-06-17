using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonMenu : MonoBehaviour
{
    bool selected = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(selected)
            {
                GetComponent<Button>().onClick.Invoke();
                selected = false;
            }
            else
            {
                GetComponent<Button>().Select();
                selected = true;
            }
        }
        else if (Input.anyKeyDown && !Input.GetKey(KeyCode.X))
        {
            selected = false;
        }
    }
}
