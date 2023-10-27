using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement_Vertical : MonoBehaviour
{
    [HideInInspector] public bool freeCamera = false;
    [HideInInspector] public bool startMovement = false;
    Transform player;
    Vector3 targetPosition;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(startMovement)
        {
            CheckMovement();
        }
    }

    void CheckMovement()
    {
        if (freeCamera)
        {
            MoveFreely();
        }
        else
        {
            SnapCamera();
        }
    }

    void MoveFreely()
    {
        targetPosition = transform.position;// Start with Larry's current position
        targetPosition.y = Mathf.Lerp(targetPosition.y, player.position.y, 2 * Time.deltaTime);
        transform.position = targetPosition; // Update Larry's position
    }

    void SnapCamera()
    {
        targetPosition = transform.position;// Start with Larry's current position
        targetPosition.y = Mathf.Lerp(targetPosition.y, ClosestDivisibleBy45(), 1 * Time.deltaTime);
        transform.position = targetPosition; // Update Larry's position
        if (Vector3.Distance(transform.localPosition, targetPosition) <= 1f)
        {
            GameObject.FindWithTag("MoveVertical").transform.localPosition = targetPosition;
        }
    }

    private float ClosestDivisibleBy45()
    {
        return Mathf.Round(player.position.y / 45.0f) * 45.0f;
    }
}
