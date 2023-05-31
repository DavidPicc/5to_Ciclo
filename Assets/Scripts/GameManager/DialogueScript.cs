using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour
{
    public static DialogueScript instance;
    public float delay = 0.1f;
    public string fullText;
    public string currentText = "";

    public TextMeshProUGUI characterText;
    public TextMeshProUGUI dialogueText;

    public bool finishedTyping = false;
    public float timeBetweenText;

    public List<string> dialogues = new List<string>();
    int index = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        characterText = GameObject.FindGameObjectWithTag("nameText").GetComponent<TextMeshProUGUI>();
        dialogueText = GameObject.FindGameObjectWithTag("dialogueText").GetComponent<TextMeshProUGUI>();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    bool isRunning = false;
    public IEnumerator StartDialogue()
    {
        isRunning = true;
        for(int i = 0; i < dialogues.Count; i++)
        {
            finishedTyping = false;
            dialogueText.text = "";
            fullText = dialogues[i].Substring(dialogues[i].IndexOf(';') + 1);

            string speaker = dialogues[i].Substring(0, dialogues[i].IndexOf(';'));
            //string text = dialogues[i].Substring(dialogues[i].IndexOf(';') + 1);
            characterText.text = speaker;
            foreach (char c in fullText)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(delay);
                if (dialogueText.text.Length >= fullText.Length)
                {
                    yield return new WaitForSeconds(timeBetweenText);
                    FinishedTyping();
                }
            }
        }
        isRunning = false;
    }

    public void FinishedTyping()
    {
        finishedTyping = true;
        if(dialogues.Count == index+1)
        {
            CanvasUI.instance.dialogueObject.SetActive(false);
            dialogues.Clear();
            index = 0;
        }
        else
        {
            index += 1;
        }
    }

    public void SetDialogue(string[] dialogue)
    {
        CanvasUI.instance.dialogueObject.SetActive(true);
        for(int i = 0; i < dialogue.Length; i++)
        {
            dialogues.Add(dialogue[i]);
        }
        //fullText = dialogue;
        if(!isRunning)
            StartCoroutine(StartDialogue());
    }

    public void SetDialogue(string dialogue)
    {
        CanvasUI.instance.dialogueObject.SetActive(true);
        dialogues.Add(dialogue);
        //fullText = dialogue;
        if (!isRunning)
            StartCoroutine(StartDialogue());
    }

    //public bool running = false;
    //public IEnumerator ShowText()
    //{
    //    running = true;
    //    if(running)
    //    {
    //        for (int i = 0; i < fullText.Length + 1; i++)
    //        {
    //            currentText = fullText.Substring(0, i);
    //            CanvasUI.instance.dialogueText.text = currentText;
    //            yield return new WaitForSeconds(delay);
    //        }
    //    }

    //    running = false;
    //}
}
