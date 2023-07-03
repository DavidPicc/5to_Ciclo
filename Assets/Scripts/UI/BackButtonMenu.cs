using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonMenu : MonoBehaviour
{
    public bool selected = false;
    public bool isResumeButton = false;
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.X))
        //{
        //    if(FindObjectOfType<UIPriority>().volumeSelected == null)
        //    {
        //        Debug.Log("NULO");
        //        if (selected && !isResumeButton)
        //        {
        //            GetComponent<Button>().onClick.Invoke();
        //            selected = false;
        //        }
        //        else
        //        {
        //            GetComponent<Button>().Select();
        //            selected = true;
        //        }
        //    }
            
        //}
        //else if (Input.anyKeyDown && !Input.GetKey(KeyCode.X))
        //{
        //    selected = false;
        //}
    }

    public void SelectButton()
    {
        if (selected && !isResumeButton)
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
}
