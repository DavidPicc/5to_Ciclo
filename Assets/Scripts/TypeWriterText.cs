using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TypeWriterText : MonoBehaviour
{
    public static TypeWriterText instance;
    public float delay = 0.1f;
    public string fullText;
    public string currentText = "";

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //fullText = CanvasUI.instance.dialogueText.text;
    }

    public bool running = false;
    public IEnumerator ShowText()
    {
        running = true;
        if(running)
        {
            for (int i = 0; i < fullText.Length + 1; i++)
            {
                currentText = fullText.Substring(0, i);
                CanvasUI.instance.dialogueText.text = currentText;
                yield return new WaitForSeconds(delay);
            }
        }
        
        running = false;
    }
}
