using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_TestSalvador2 : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] public AudioSource audioManager;
    [SerializeField] AudioClip MovementSound;

    [Header("Movement")]
    public float accelerationX;
    public float maxSpeed;
    public float speedY;
    public float dragFactorX;
    public float dragFactorY;
    public bool changingDirectionsX => (input.x < 0 && rb.velocity.x > 0) || (input.x > 0 && rb.velocity.x < 0);
    public bool changingDirectionsY => (input.y < 0 && rb.velocity.y > 0) || (input.y > 0 && rb.velocity.y < 0);

    [Header("Input")]
    [SerializeField] Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //AudioManager.instance.PlaySFX(audioManager, MovementSound, 0.5f);
    }

    void Update()
    {
        GetInput();

        if (rb.velocity.sqrMagnitude > 1f)
        {
            audioManager.volume = 0;
        }
        else
        {
            audioManager.volume = 1f;
        }
    }

    private void FixedUpdate()
    {
        Movement();
        Friction();
    }

    private void Movement()
    {
        if (input.x != 0) rb.AddForce(Vector3.right * input.x * accelerationX);

        rb.velocity = new Vector3(rb.velocity.x, input.y * speedY, rb.velocity.z);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y, rb.velocity.z);
        }
    }

    private void Friction()
    {
        if (input.x == 0 || changingDirectionsX)
        {
            rb.AddForce(Vector3.left * rb.velocity.x * dragFactorX);
        }

        if (input.y == 0 || changingDirectionsY)
        {
            rb.AddForce(Vector3.down * rb.velocity.y * dragFactorY);
        }
    }

    private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Cores")&& !done)
    //    {
    //        audioManager.PlayOneShot(MovementSound);
    //        GameScore.instance.AddCores(1);
    //        Destroy(other.gameObject);
    //        done = true;
    //    }
    //}
}
