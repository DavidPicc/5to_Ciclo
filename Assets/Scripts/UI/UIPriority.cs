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
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(FindObjectOfType<GameManager>() != null)
            {
                if (GameManager.instance.shopMenu.activeSelf)
                {
                    GameManager.instance.ResumeGame();
                }
                else
                {
                    if (volumeSelected != null)
                    {
                        volumeSelected.GetComponent<VolumeButtonScript>().DeactivateButtons();
                    }
                    else
                    {
                        if (FindObjectOfType<BackButtonMenu>() != null)
                        {
                            FindObjectOfType<BackButtonMenu>().SelectButton();
                        }
                    }
                }
            }
            else
            {
                if (volumeSelected != null)
                {
                    volumeSelected.GetComponent<VolumeButtonScript>().DeactivateButtons();
                }
                else
                {
                    if (FindObjectOfType<BackButtonMenu>() != null)
                    {
                        FindObjectOfType<BackButtonMenu>().SelectButton();
                    }
                }
            }
            
        }
        else if (Input.anyKeyDown && (!Input.GetKeyDown(KeyCode.X) || !Input.GetKeyDown(KeyCode.Escape)))
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
