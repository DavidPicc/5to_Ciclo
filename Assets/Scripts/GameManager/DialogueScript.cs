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
    public IEnumerator StartDialogue2(AudioSource audioSource)
    {
        isRunning = true;
        for (int i = 0; i < dialogues.Count; i++)
        {
            finishedTyping = false;
            dialogueText.text = "";
            fullText = dialogues[i].Substring(dialogues[i].IndexOf(';') + 1);

            string speaker = dialogues[i].Substring(0, dialogues[i].IndexOf(';'));
            characterText.text = speaker;
            foreach (char c in fullText)
            {
                if (GameManager.instance.isPaused) // If the game is paused, wait for it to be UNPAUSED before continuing.
                {
                    yield return new WaitUntil(() => !GameManager.instance.isPaused);
                }

                dialogueText.text += c;

                float pitch = Random.Range(0.5f, 1.5f);
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(textSound);

                yield return new WaitForSecondsRealtime(delay);
                if (dialogueText.text.Length >= fullText.Length)
                {
                    yield return new WaitForSecondsRealtime(timeBetweenText);
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

    public void SetDialogue(string[] dialogue, AudioSource audioSource)
    {
        CanvasUI.instance.dialogueObject.SetActive(true);
        for(int i = 0; i < dialogue.Length; i++)
        {
            dialogues.Add(dialogue[i]);
        }
        if (!isRunning) //StartDialogue(audioSource);
            StartCoroutine(StartDialogue2(audioSource));
    }

    public void SetDialogue(string dialogue, AudioSource audioSource)
    {
        CanvasUI.instance.dialogueObject.SetActive(true);
        dialogues.Add(dialogue);
        if (!isRunning) //StartDialogue(audioSource);
            StartCoroutine(StartDialogue2(audioSource));
    }

    private void StartDialogue(AudioSource audioSource)
    {
        isRunning = true;
        NextDialogue(audioSource);
    }

    private void NextDialogue(AudioSource audioSource)
    {
        if (index < dialogues.Count)
        {
            finishedTyping = false;
            string[] dialogueParts = dialogues[index].Split(';');
            characterText.text = dialogueParts[0];
            StartCoroutine(TypeText(dialogueParts[1], audioSource));
            index++;
        }
        else
        {
            isRunning = false;
            // Handle the end of the dialogue.
        }
    }

    private IEnumerator TypeText(string text, AudioSource audioSource)
    {
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            float pitch = Random.Range(0.5f, 1.5f);
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(textSound);

            if(Time.timeScale > 0)
            {
                yield return new WaitForSeconds(delay);
                if (dialogueText.text.Length >= text.Length)
                {
                    FinishTyping();
                }
            }
            
            
        }
    }

    private void FinishTyping()
    {
        finishedTyping = true;
    }
}
