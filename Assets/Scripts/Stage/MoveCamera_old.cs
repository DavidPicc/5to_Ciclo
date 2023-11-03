using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera_old : MonoBehaviour
{
    bool startMoving = false;
    Vector3 finalPosition;
    [SerializeField] float moveSpeed;
    Vector3 initialPosition;
    [SerializeField] float verticalInput;

    StageMovement_Vertical moveVertical;

    void Start()
    {
        moveVertical = FindObjectOfType<StageMovement_Vertical>();
    }

    void Update()
    {
        if (startMoving)
        {
            float step = Time.deltaTime * moveSpeed;
            moveVertical.transform.localPosition = Vector3.MoveTowards(moveVertical.transform.localPosition, finalPosition, step);

            if (Vector3.Distance(moveVertical.transform.localPosition, finalPosition) <= 0.5f)
            {
                moveVertical.transform.localPosition = finalPosition;
                startMoving = false;
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !startMoving)
        {
            if(moveVertical.freeCamera || moveVertical.startMovement)
            {
                moveVertical.player.parent = moveVertical.transform;
                moveVertical.freeCamera = false;
                moveVertical.startMovement = false;
            }
            startMoving = true;
            initialPosition = moveVertical.currentCameraPosition;
            finalPosition = initialPosition + new Vector3(0, verticalInput, 0);
            moveVertical.currentCameraPosition = finalPosition;
        }
    }
}
