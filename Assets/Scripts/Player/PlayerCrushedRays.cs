using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrushedRays : MonoBehaviour
{
    public LayerMask crushedLayers;
    public float checkDistance;
    public float maxAngleDeviation;

    public Transform leftPoint;
    public Transform rightPoint;

    RaycastHit leftHit;
    RaycastHit rightHit;
    
    void Update()
    {
        CheckCrushed();
    }

    private void CheckCrushed()
    {
        FindObjectOfType<Player_Health>().crushed = 0;

        if (Physics.Raycast(leftPoint.position, -Vector3.right, out leftHit, checkDistance, crushedLayers))
        {
            if(leftHit.collider.gameObject.CompareTag("Obstacle") || (((1 << leftHit.collider.gameObject.layer) & crushedLayers) != 0))
            {
                float angle = Mathf.Acos(Vector3.Dot(leftHit.normal, Vector3.right)) * Mathf.Rad2Deg;
                if (angle < maxAngleDeviation) FindObjectOfType<Player_Health>().crushed += 1;
            }
        }

        if (Physics.Raycast(rightPoint.position, Vector3.right, out rightHit, checkDistance, crushedLayers))
        {
            if (rightHit.collider.gameObject.CompareTag("Obstacle") || (((1 << rightHit.collider.gameObject.layer) & crushedLayers) != 0))
            {
                float angle = Mathf.Acos(Vector3.Dot(rightHit.normal, -Vector3.right)) * Mathf.Rad2Deg;
                if (angle < maxAngleDeviation) FindObjectOfType<Player_Health>().crushed += 1;
            }
        }

        if (FindObjectOfType<Player_Health>().crushed >= 2)
        {
            FindObjectOfType<Player_Health>().Death();
        }
    }
}
