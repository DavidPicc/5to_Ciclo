using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] AudioClip MovementSound;
    public enum MovementType
    {
        drag,
        noDrag
    }
    public MovementType moveType;
    [SerializeField] float acceleration;
    [SerializeField] float speedLimit;
    float moveX, moveY;
    [SerializeField] float moveDrag, stayDrag;
    [HideInInspector] public Vector3 moveVector;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveVector = new Vector3(moveX, moveY, 0).normalized;
    }

    void FixedUpdate()
    {
        switch(moveType)
        {
            case MovementType.drag:
                Movement_WithDrag();
                break;
            case MovementType.noDrag:
                Movement_WithoutDrag();
                break;
        }
        Drag();
    }

    public void Movement_WithDrag()
    {
        if (Mathf.Abs(rb.velocity.x) < speedLimit)
            rb.AddForce(moveVector.x * Vector3.right * acceleration, ForceMode.Acceleration);
        if (Mathf.Abs(rb.velocity.y) < speedLimit)
            rb.AddForce(moveVector.y * Vector3.up * acceleration, ForceMode.Acceleration);
        AudioManager.instance.PlaySFX(audioManager, MovementSound, 0.5f);
    }

    public void Movement_WithoutDrag()
    {
        AudioManager.instance.PlaySFXOnce(audioManager, MovementSound, 0.5f);
        rb.velocity = moveVector * speedLimit;
    }

    public void Drag()
    {
        if(moveX != 0 || moveY != 0)
        {
            rb.drag = moveDrag;
        }
        else
        {
            rb.drag = stayDrag;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cores"))
        {
            if (other.GetComponent<AudioSource>() != null && other.GetComponent<Cores_Script>() != null && other.GetComponent<Cores_Script>().CoresSound != null)
            {
                other.GetComponent<AudioSource>().PlayOneShot(other.GetComponent<Cores_Script>().CoresSound);
            }
            
            GameScore.instance.AddCores(1);
            Destroy(other.gameObject);
        }
    }
}
