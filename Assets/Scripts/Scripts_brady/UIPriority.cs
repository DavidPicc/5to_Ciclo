using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;
using System;


public class UIPriority : MonoBehaviour
{
    //public CinemachineVirtualCamera currentCamera;

    //public GameObject optionsMenu;
    public GameObject volumeSelected;

    void Start()
    {
        //currentCamera.Priority++;
    }

    void Update()
    {
        //if (optionsMenu.activeSelf && volumeSelected != null)
        if (Input.GetKeyDown(KeyCode.X))
        {
            if(volumeSelected != null)
            {
                volumeSelected.GetComponent<VolumeButtonScript>().DeactivateButtons();
            }
            else
            {
                if(FindObjectOfType<BackButtonMenu>() != null)
                {
                    FindObjectOfType<BackButtonMenu>().SelectButton();
                }  
            }
        }
        else if (Input.anyKeyDown && !Input.GetKey(KeyCode.X))
        {
            if(FindObjectOfType<BackButtonMenu>() != null && FindObjectOfType<BackButtonMenu>().selected)
            {
                FindObjectOfType<BackButtonMenu>().selected = false;
            }   
        }
    }

    //public void UpdateCamera(CinemachineVirtualCamera target)
    //{
    //    currentCamera.Priority--;
    //    currentCamera = target;
    //    currentCamera.Priority++;
    //}
}
