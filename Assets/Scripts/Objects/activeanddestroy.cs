using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeanddestroy : MonoBehaviour
{
    public GameObject objectToActivate;
    public float destructionDelay = 7f;
    public float timeactive;
    private void Start()
    {
        Invoke("ActivateObject", timeactive);
        Invoke("DestroyObject", destructionDelay);
    }

    private void ActivateObject()
    {
        objectToActivate.SetActive(true);
    }

    private void DestroyObject()
    {
        Destroy(objectToActivate);
    }

}
