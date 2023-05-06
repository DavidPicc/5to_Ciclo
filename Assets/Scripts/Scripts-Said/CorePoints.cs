using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CorePoints : MonoBehaviour
{
    private float cores;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textMesh.text = cores.ToString("0"); 
    }

    public void AddCore(float enterCore)
    {
        cores += enterCore;
    }
}
