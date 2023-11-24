using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] GameObject endTransition;

    public void PlayTransition()
    {
        endTransition.SetActive(true);
    }     
}
