using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameScore : MonoBehaviour
{
    public static GameScore instance;
    GameObject player;
    public int gearScore, coreScore;
    //[SerializeField] public AudioSource audioManager;
    [SerializeField] public AudioClip GearSound, CoreSound;

    [SerializeField] public int playerLevel = 0;
    [SerializeField] public int scoreToLevelUp;
    //public int invisibleScore;

    private void Awake()
    {
        //audioManager = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }

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
        CanvasUI.instance.gearsText.text = gearScore.ToString();
        CanvasUI.instance.coresText.text = coreScore.ToString();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(FindObjectOfType<LevelUnlock>() != null) StartCoroutine(InvokeIfUnlockedLevel());
        //AddLevel();
    }

    IEnumerator InvokeIfUnlockedLevel()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        if (LevelUnlock.Instance.loadedUnlockedLevel)
        {
            AddGears(10000);
            AddCores(5);
        }
    }

    void Update()
    {
        //if(scoreToNextLevel >= scoreToLevelUp)
        //{
        //    AddLevel();
        //}
    }

    public void AddGears(int amount)
    {
        gearScore = gearScore + amount;
        AudioManager.instance.PlaySFX(GetComponent<AudioSource>(), GearSound, 1f);
        //Debug.Log("SONIDO");
        //invisibleScore = invisibleScore + amount;
        CanvasUI.instance.gearsText.text = gearScore.ToString();
    }

    public void RemoveGears(int amount)
    {
        gearScore = gearScore - amount;
        CanvasUI.instance.gearsText.text = gearScore.ToString();
    }

    public void AddCores(int amount)
    {
        coreScore = coreScore + amount;
        AudioManager.instance.PlaySFX(GetComponent<AudioSource>(), CoreSound, 1f);
        CanvasUI.instance.coresText.text = coreScore.ToString();
    }

    public void RemoveCores(int amount)
    {
        coreScore = coreScore - amount;
        CanvasUI.instance.coresText.text = coreScore.ToString();
    }

    //public void AddLevel()
    //{
    //    playerLevel++;
    //    CanvasUI.instance.playerLevelText.text = "Player level: " + playerLevel.ToString();
    //    scoreToLevelUp += 30; // Para el siguiente nivel tienes que hacer 30 puntos m�s que en el anterior.
    //}
}
