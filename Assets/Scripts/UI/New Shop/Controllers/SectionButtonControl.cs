using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionButtonControl : MonoBehaviour
{
    bool selected;
    public bool Selected { get { return selected; } set { selected = value; } }

    bool hover;
    public bool Hover { get { return hover; } set { hover = value; } }

    [Header("Navigation")]
    [SerializeField] SectionButtonControl buttonAtRight;
    [SerializeField] SectionButtonControl buttonAtLeft;
    [SerializeField] SectionButtonControl buttonUpwards;
    [SerializeField] SectionButtonControl buttonDownwards;

    public SectionButtonControl GetButtonAtRight()
    {
        return buttonAtRight;
    }

    public SectionButtonControl GetButtonAtLeft()
    {
        return buttonAtLeft;
    }

    public SectionButtonControl GetButtonUpwards()
    {
        return buttonUpwards;
    }

    public SectionButtonControl GetButtonDownwards()
    {
        return buttonDownwards;
    }
}
