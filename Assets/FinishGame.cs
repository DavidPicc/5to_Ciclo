using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] GameObject endTransition;

    [SerializeField] GameObject phaseTransition;

    public void PlayTransition()
    {
        endTransition.SetActive(true);
    }

    public void PlayPhaseTransition()
    {
        phaseTransition.SetActive(true);
    }
}
