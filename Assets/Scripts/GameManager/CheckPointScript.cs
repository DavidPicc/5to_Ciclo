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

    public bool tryingThings = false;

    private void Awake()
    {
        instance = this;
        //PlayerPrefs.SetInt("wave", 0);
    }

    void Start()
    {
        /// PARA RESETEAR LOS PLAYERPREFS (INICIAR PLAY CON EL BOOL ACTIVADO Y DETENERLO, DESACTIVAR EL BOOL Y EMPEZAR A JUGAR) (BORRAR LUEGO) ///
        //if(resetCheckpoint)
        //{
        //    ResetCheckpoints();
        //}
        if(!tryingThings)
        {
            savedWave = PlayerPrefs.GetInt("wave");
        }
        else
        {
            savedWave = SituationManager.instance.waveIndex;
        }
        //savedPoint = new Vector3(PlayerPrefs.GetFloat("pointX"), PlayerPrefs.GetFloat("pointY"), PlayerPrefs.GetFloat("pointZ"));
        gearsSaved = PlayerPrefs.GetInt("gears");
        coresSaved = PlayerPrefs.GetInt("cores");

        LoadCheckpoints();

        Debug.Log("WAVE: " + savedWave);
    }

    public void LoadCheckpoints()
    {
        SituationManager.instance.waveIndex = savedWave;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateCheckpoints();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //ResetCheckpoints();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            //Debug.Log("CHECKPOINT: " + savedPoint.x + ", " + savedPoint.y + ", " + savedPoint.z);
            Debug.Log("WAVE: " + savedWave);
        }
    }

    public void ResetCheckpoints()
    {
        savedWave = 0;

        PlayerPrefs.DeleteAll();
    }
    public void UpdateCheckpoints()
    {
        savedWave = SituationManager.instance.currentWave;
        gearsSaved = GameScore.instance.gearScore;
        coresSaved = GameScore.instance.coreScore;

        PlayerPrefs.SetInt("wave", savedWave);
        PlayerPrefs.SetInt("gears", gearsSaved);
        PlayerPrefs.SetInt("cores", coresSaved);

        FindObjectOfType<Player_Health>().GetFullHealth();

        Debug.Log("WAVE SAVED: " + savedWave);
    }
}
