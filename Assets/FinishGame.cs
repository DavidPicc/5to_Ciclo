using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    [SerializeField] GameObject endTransition;

    public GameManager manager;

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
