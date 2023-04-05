using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dodge : MonoBehaviour
{
    [SerializeField] GameObject dodgeVisual;
    [SerializeField] float dodgeTime;
    float timer;
    bool canDodge => Input.GetMouseButtonDown(1) && !isDodging;
    [SerializeField] float dodgeForce;
    public bool isDodging = false;
    float extraHelp = 1;
    void Start()
    {
        dodgeVisual.SetActive(false);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && canDodge)
        {
            StartCoroutine(Dodge());
        }
    }

    public IEnumerator Dodge()
    {
        isDodging = true;
        dodgeVisual.SetActive(true);
        yield return new WaitForSeconds(0.05f);

        FindObjectOfType<CameraShake>().ShakeCamera(2f, 0.15f);

        if (GetComponent<Player_Movement>().moveType == Player_Movement.MovementType.noDrag)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            extraHelp = 2f;
        }
        else
        {
            extraHelp = 1f;
        }

        if(GetComponent<Player_Movement>().moveVector.magnitude <= 0)
        {
            GetComponent<Rigidbody>().AddForce(transform.right * dodgeForce * 2.5f * extraHelp, ForceMode.Impulse);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(GetComponent<Player_Movement>().moveVector * dodgeForce * extraHelp, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(dodgeTime - 0.05f);
        isDodging= false;
        dodgeVisual.SetActive(false);
    }
}
