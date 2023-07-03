using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollowPlayer : MonoBehaviour
{
    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(playerTransform.position);
        }
    }
}
