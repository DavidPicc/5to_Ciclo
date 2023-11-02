using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera_old : MonoBehaviour
{
    [SerializeField] bool startMoving = false;
    [SerializeField] Vector3 finalPosition;
    [SerializeField] float moveSpeed;

    [SerializeField] bool addToPosition = false;
    Vector3 initialPosition;
    [SerializeField] float verticalInput;

    Transform moveVertical;

    void Start()
    {
        moveVertical = GameObject.FindWithTag("MoveVertical").transform;
    }

    void Update()
    {
        if (startMoving)
        {
            float step = Time.deltaTime * moveSpeed;

            //GameObject.FindWithTag("MoveVertical").transform.localPosition = Vector3.Lerp(GameObject.FindWithTag("MoveVertical").transform.localPosition, finalPosition, fractionOfJourney);
            moveVertical.localPosition = Vector3.MoveTowards(moveVertical.localPosition, finalPosition, step);

            if (Vector3.Distance(moveVertical.localPosition, finalPosition) <= 1.2f)
            {
                moveVertical.localPosition = finalPosition;
                startMoving = false;
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !startMoving)
        {
            startMoving = true;
            if (addToPosition)
            {
                initialPosition = moveVertical.localPosition;
                finalPosition = initialPosition + new Vector3(0, verticalInput, 0);
            }

        }
    }
}
