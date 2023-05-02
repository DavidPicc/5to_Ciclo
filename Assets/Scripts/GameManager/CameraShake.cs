using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera virtualCam;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeIntensity;
    float timer;
    CinemachineBasicMultiChannelPerlin channelPerlin;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        
    }

    void Start()
    {
        StopShake();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        CinemachineBasicMultiChannelPerlin _channelPerlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _channelPerlin.m_AmplitudeGain = intensity;
        timer = duration;
    }

    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _channelPerlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _channelPerlin.m_AmplitudeGain = 0;
        timer = 0;
    }
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    ShakeCamera(shakeIntensity, shakeDuration);
        //}

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            StopShake();
        }
    }
}
