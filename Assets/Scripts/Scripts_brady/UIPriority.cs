using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;


public class UIPriority : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;

    public GameObject optionsMenu;
    public GameObject volumeSelected;

    void Start()
    {
        currentCamera.Priority++;
    }

    void Update()
    {
        if(optionsMenu.activeSelf && volumeSelected != null)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                volumeSelected.GetComponent<VolumeButtonScript>().DeactivateButtons();
            }
        }
    }

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;
        currentCamera = target;
        currentCamera.Priority++;
    }
}
