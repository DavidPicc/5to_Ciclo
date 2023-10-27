using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //[SerializeField] bool startMoving = false;
    //[SerializeField] Vector3 finalPosition;
    //[SerializeField] float moveSpeed;

    //[SerializeField] bool addToPosition = false;
    //Vector3 initialPosition;
    //[SerializeField] float verticalInput;

    bool freeCamera = false;
    Transform player;
    StageMovement_Vertical moveVertical;
    Transform moveHorizontal;
    Vector3 targetPosition;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        moveVertical = FindObjectOfType<StageMovement_Vertical>();
        moveHorizontal = FindObjectOfType<StageMovement>().transform;
    }

    void Update()
    {
        //if (startMoving)
        //{
        //    moveVertical.position = new Vector3(moveVertical.position.x, player.transform.position.y, moveVertical.position.z);
        //    StartCoroutine(FollowPlayerY());
        //    MoveFreely();
        //}
        //else
        //{
        //    SnapCamera();
        //}

        //if (startMoving)
        //{
        //    float step = Time.deltaTime * moveSpeed;

        //    //GameObject.FindWithTag("MoveVertical").transform.localPosition = Vector3.Lerp(GameObject.FindWithTag("MoveVertical").transform.localPosition, finalPosition, fractionOfJourney);
        //    GameObject.FindWithTag("MoveVertical").transform.localPosition = Vector3.MoveTowards(GameObject.FindWithTag("MoveVertical").transform.localPosition, finalPosition, step);

        //    if(Vector3.Distance(GameObject.FindWithTag("MoveVertical").transform.localPosition, finalPosition) <= 1.2f)
        //    {
        //        GameObject.FindWithTag("MoveVertical").transform.localPosition = finalPosition;
        //        startMoving = false;
        //        Destroy(gameObject);
        //    }

        //}
    }

    //void MoveFreely()
    //{
    //    targetPosition = moveVertical.position;// Start with Larry's current position
    //    targetPosition.y = Mathf.Lerp(targetPosition.y, player.position.y, 2 * Time.deltaTime);
    //    moveVertical.position = targetPosition; // Update Larry's position
    //}

    //void SnapCamera()
    //{
    //    targetPosition = moveVertical.position;// Start with Larry's current position
    //    targetPosition.y = Mathf.Lerp(targetPosition.y, ClosestDivisibleBy45(), 2 * Time.deltaTime);
    //    moveVertical.position = targetPosition; // Update Larry's position
    //    if (Vector3.Distance(moveVertical.transform.localPosition, targetPosition) <= 1f)
    //    {
    //        GameObject.FindWithTag("MoveVertical").transform.localPosition = targetPosition;
    //        startMoving = false;
    //        Destroy(gameObject);
    //    }
    //}

    //private float ClosestDivisibleBy45()
    //{
    //     return Mathf.Round(player.position.y / 45.0f) * 45.0f;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //StartCameraMove();
            if(!moveVertical.startMovement)
            {
                player.parent = moveHorizontal;
                moveVertical.freeCamera = true;
                moveVertical.startMovement = true;
            }
            else
            {
                player.parent = moveVertical.transform;
                moveVertical.freeCamera = false;
            }
            //if(startMoving)
            //{
            //    player.parent = moveHorizontal;
            //    moveVertical.freeCamera = true;
            //    moveVertical.startMovement = true;
            //}
            //else
            //{
            //    player.parent = moveVertical.transform;
            //    moveVertical.freeCamera = false;
            //}
            Destroy(gameObject);
        }
    }

    //void StartCameraMove()
    //{
    //    timerToActivate = 0;
    //    startMoving = true;
    //    if (addToPosition)
    //    {
    //        initialPosition = GameObject.FindWithTag("MoveVertical").transform.localPosition;
    //        finalPosition = initialPosition + new Vector3(0, verticalInput, 0);
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        if(verticalInput > 0) // Si vas hacia arriba.
    //        {
    //            if(Input.GetAxisRaw("Vertical") > 0)
    //            {
    //                timerToActivate += Time.deltaTime;
    //                if(timerToActivate >= 1f)
    //                {
    //                    StartCameraMove();
    //                }
    //            }
    //            else
    //            {
    //                timerToActivate = 0;
    //            }
    //        }
    //        else // Si vas hacia abajo.
    //        {
    //            if (Input.GetAxisRaw("Vertical") < 0)
    //            {
    //                timerToActivate += Time.deltaTime;
    //                if (timerToActivate >= 1f)
    //                {
    //                    StartCameraMove();
    //                }
    //            }
    //            else
    //            {
    //                timerToActivate = 0;
    //            }
    //        }
    //    }
    //}
}
