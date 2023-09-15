using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] bool startMoving = false;
    [SerializeField] Vector3 finalPosition;
    [SerializeField] float moveSpeed;

    [SerializeField] bool addToPosition = false;
    Vector3 initialPosition;
    [SerializeField] float verticalInput;

    void Start()
    {
        
    }

    void Update()
    {

        if (startMoving)
        {
            float step = Time.deltaTime * moveSpeed;

            //GameObject.FindWithTag("MoveVertical").transform.localPosition = Vector3.Lerp(GameObject.FindWithTag("MoveVertical").transform.localPosition, finalPosition, fractionOfJourney);
            GameObject.FindWithTag("MoveVertical").transform.localPosition = Vector3.MoveTowards(GameObject.FindWithTag("MoveVertical").transform.localPosition, finalPosition, step);

            if(Vector3.Distance(GameObject.FindWithTag("MoveVertical").transform.localPosition, finalPosition) <= 1.2f)
            {
                GameObject.FindWithTag("MoveVertical").transform.localPosition = finalPosition;
                startMoving = false;
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !startMoving)
        {
            startMoving = true;
            if(addToPosition)
            {
                initialPosition = GameObject.FindWithTag("MoveVertical").transform.localPosition;
                finalPosition = initialPosition + new Vector3(0, verticalInput, 0);
            }

        }
    }
}
