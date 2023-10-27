using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement_Vertical : MonoBehaviour
{
    public bool freeCamera = false;
    public bool startMovement = false;
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
        Vector3 target = new Vector3(transform.position.x, ClosestDivisibleBy45(), transform.position.z);
        if (Vector3.Distance(transform.position, target) <= 0.3f)
        {
            transform.position = target;
            startMovement = false;
            Debug.Log("akldfnasd");
        }
    }

    private float ClosestDivisibleBy45()
    {
        return Mathf.Round(player.position.y / 45.0f) * 45.0f;
    }
}
