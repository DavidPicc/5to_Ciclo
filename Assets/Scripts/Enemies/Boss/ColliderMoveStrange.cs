using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMoveStrange : MonoBehaviour
{

    public Transform[] movePoints;
    [SerializeField] float moveSpeed = 2f;
    private int currentMovePointIndex = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (currentMovePointIndex >= movePoints.Length)
        {
            currentMovePointIndex = 0;
        }

        Transform nextMovePoint = movePoints[currentMovePointIndex];

        transform.position = Vector3.MoveTowards(transform.position, nextMovePoint.position, moveSpeed * Time.deltaTime);

        if (transform.position == nextMovePoint.position)
        {
            currentMovePointIndex++;
        }
    }
}
