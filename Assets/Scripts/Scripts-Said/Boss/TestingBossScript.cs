using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestingBossScript : MonoBehaviour
{
    private Rigidbody rb;
    public float lifeBoss;
    [SerializeField] float halfthealt;
    [SerializeField] float moveSpeed = 2f;
    public Transform[] movePoints;
    private int currentMovePointIndex = 0;

    public Phase1 phase1;
    //public Phase2 phase2;
    //public Phase3 phase3;
    //public Phase4 phase4;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (lifeBoss == 100)
        {
            phase1.enabled = true;
            /*phase2.enabled = false;
            phase3.enabled = false;
            phase4.enabled = false;*/
        }
        if (lifeBoss == 75)
        {
            phase1.enabled = false;
            /*phase2.enabled = true;
            phase3.enabled = false;
            phase4.enabled = false;*/
        }
        if (lifeBoss == 50)
        {
            phase1.enabled = false;
           /* phase2.enabled = false;
            phase3.enabled = true;
            phase4.enabled = false;*/
        }
        if (lifeBoss == 25)
        {
            phase1.enabled = false;
           /* phase2.enabled = false;
            phase3.enabled = false;
            phase4.enabled = true;*/
        }
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
