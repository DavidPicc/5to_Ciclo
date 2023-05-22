using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public static CheckPointScript instance;
    Transform stage;
    public static Vector3 savedPoint;
    public static int savedWave;
    public static int gearsSaved;
    public static int coresSaved;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        /// PARA RESETEAR LOS PLAYERPREFS SIEMPRE (BORRAR LUEGO) ///
        ResetCheckpoints();


        savedWave = PlayerPrefs.GetInt("wave");
        //savedPoint = new Vector3(PlayerPrefs.GetFloat("pointX"), PlayerPrefs.GetFloat("pointY"), PlayerPrefs.GetFloat("pointZ"));
        gearsSaved = PlayerPrefs.GetInt("gears");
        coresSaved = PlayerPrefs.GetInt("cores");

        LoadCheckpoints();

        Debug.Log("CHECKPOINT: " + savedPoint.x + ", " + savedPoint.y + ", " + savedPoint.z);
        Debug.Log("WAVE: " + savedWave);

        //if (FindObjectOfType<Player_Movement>() != null)
        //{
        //    if(savedPoint == Vector3.zero)
        //    {
        //        player = FindObjectOfType<Player_Movement>().transform;
        //        //savedPoint = player.position;
        //    }
        //}
    }

    public void LoadCheckpoints()
    {
        SituationManager.instance.wave = savedWave;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateCheckpoints();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCheckpoints();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            //Debug.Log("CHECKPOINT: " + savedPoint.x + ", " + savedPoint.y + ", " + savedPoint.z);
            Debug.Log("WAVE: " + savedWave);
        }
    }

    public void ResetCheckpoints()
    {
        savedPoint = Vector3.zero;
        savedWave = 0;

        PlayerPrefs.DeleteAll();
    }
    public void UpdateCheckpoints()
    {
        //savedPoint = player.position;
        savedWave = SituationManager.instance.wave;
        gearsSaved = GameScore.instance.gearScore;
        coresSaved = GameScore.instance.coreScore;

        PlayerPrefs.SetInt("wave", savedWave);
        //PlayerPrefs.SetFloat("pointX", savedPoint.x);
        //PlayerPrefs.SetFloat("pointY", savedPoint.y);
        //PlayerPrefs.SetFloat("pointZ", savedPoint.z);
        PlayerPrefs.SetInt("gears", gearsSaved);
        PlayerPrefs.SetInt("cores", coresSaved);

        //Debug.Log("CHECKPOINT SAVED: " + savedPoint.x + ", " + savedPoint.y + ", " + savedPoint.z);
        Debug.Log("WAVE SAVED: " + savedWave);
    }
}
