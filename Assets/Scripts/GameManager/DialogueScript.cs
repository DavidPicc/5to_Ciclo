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
    public float delay = 1f;
    public string fullText;
    public string currentText = "";

    public TextMeshProUGUI characterText;
    public TextMeshProUGUI dialogueText;

    public bool finishedTyping = false;
    public float timeBetweenText;

    public List<string> dialogues = new List<string>();
    int index = 0;

    [SerializeField] AudioClip textSound;

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
        instance = this;
    }

    void Start()
    {
        CanvasUI.instance.dialogueObject.SetActive(false);
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
            characterText.text = speaker;
            foreach (char c in fullText)
            {
                dialogueText.text += c;

                float pitch = Random.Range(0.5f, 1.5f);
                GetComponent<AudioSource>().pitch = pitch;
                GetComponent<AudioSource>().PlayOneShot(textSound);

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
        if(!isRunning)
            StartCoroutine(StartDialogue());
    }

    public void SetDialogue(string dialogue)
    {
        CanvasUI.instance.dialogueObject.SetActive(true);
        dialogues.Add(dialogue);
        if (!isRunning)
            StartCoroutine(StartDialogue());
    }
}
