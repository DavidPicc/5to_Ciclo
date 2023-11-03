using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement_Vertical : MonoBehaviour
{
    public bool freeCamera = false;
    public bool startMovement = false;
    [HideInInspector] public Transform player;
    Vector3 targetPosition;
    public Vector3 currentCameraPosition;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        currentCameraPosition = transform.localPosition;
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
        targetPosition = transform.position;
        targetPosition.y = Mathf.Lerp(targetPosition.y, player.position.y, 2 * Time.deltaTime);
        transform.position = targetPosition;
    }

    void SnapCamera()
    {
        targetPosition = transform.position;
        targetPosition.y = Mathf.Lerp(targetPosition.y, ClosestDivisibleBy45(), 1 * Time.deltaTime);
        transform.position = targetPosition;
        Vector3 target = new Vector3(transform.position.x, ClosestDivisibleBy45(), transform.position.z);
        if (Vector3.Distance(transform.position, target) <= 0.3f)
        {
            transform.position = target;
            currentCameraPosition = transform.localPosition;
            startMovement = false;
        }
    }

    private float ClosestDivisibleBy45()
    {
        return Mathf.Round(player.position.y / 45.0f) * 45.0f;
    }
}
