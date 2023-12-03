using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntrance : MonoBehaviour
{
    public void AnimationEvent()
    {
        FindObjectOfType<TutorialManager>().EndEntranceAnim();
    }
}
