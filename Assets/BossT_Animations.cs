using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossT_Animations : MonoBehaviour
{
    Animator anim;
    public bool bossIn = true;

    public float bossTimerIn;
    float timer;

    void Start()
    {
        anim = GetComponent<Animator>();
        timer = bossTimerIn;
        
    }

    void Update()
    {
        anim.SetBool("BossIn", bossIn);
    }
}
