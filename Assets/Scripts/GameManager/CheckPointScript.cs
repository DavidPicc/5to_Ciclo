using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public static CheckPointScript instance;
    public static Vector3 savedPoint;
    public static int savedWave;
    public static int gearsSaved;
    public static int coresSaved;
    public static float savedPointX, savedPointY;
    public static float savedCameraX, savedCameraY;
    public static float savedPlayerX, savedPlayerY;
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
        
        //savedPoint = new Vector3(PlayerPrefs.GetFloat("pointX"), PlayerPrefs.GetFloat("pointY"), PlayerPrefs.GetFloat("pointZ"));
        gearsSaved = PlayerPrefs.GetInt("gears");
        coresSaved = PlayerPrefs.GetInt("cores");

        if (!tryingThings)
        {
            //savedWave = PlayerPrefs.GetInt("wave");
            LoadCheckpoints();
        }
        else
        {
            savedWave = SituationManager.instance.waveIndex;
        }
        

        Debug.Log("WAVE: " + savedWave);
    }

    void Update()
    {
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
        //savedWave = SituationManager.instance.currentWave;
        savedWave = SituationManager.instance.waveIndex;
        gearsSaved = GameScore.instance.gearScore;
        coresSaved = GameScore.instance.coreScore;
        savedPointX = FindObjectOfType<StageMovement>().transform.position.x;
        savedPointY = FindObjectOfType<StageMovement>().transform.position.y;
        savedCameraX = GameObject.FindGameObjectWithTag("MoveVertical").transform.localPosition.x;
        savedCameraY = GameObject.FindGameObjectWithTag("MoveVertical").transform.localPosition.y;
        savedPlayerX = FindObjectOfType<Player_Health>().transform.localPosition.x;
        savedPlayerY = FindObjectOfType<Player_Health>().transform.localPosition.y;

        PlayerPrefs.SetInt("wave", savedWave);
        PlayerPrefs.SetInt("gears", gearsSaved);
        PlayerPrefs.SetInt("cores", coresSaved);
        PlayerPrefs.SetFloat("savedPointX", savedPointX);
        PlayerPrefs.SetFloat("savedPointY", savedPointY);
        PlayerPrefs.SetFloat("savedCameraX", savedCameraX);
        PlayerPrefs.SetFloat("savedCameraY", savedCameraY);
        PlayerPrefs.SetFloat("savedPlayerX", savedPlayerX);
        PlayerPrefs.SetFloat("savedPlayerY", savedPlayerY);

        FindObjectOfType<Player_Health>().GetFullHealth();

        Debug.Log("WAVE SAVED: " + savedWave);
        Debug.Log("Player position: " + FindObjectOfType<Player_Health>().transform.position);
    }

    public void LoadCheckpoints()
    {
        //SituationManager.instance.waveIndex = savedWave;
        FindObjectOfType<StageMovement>().transform.position = new Vector3(PlayerPrefs.GetFloat("savedPointX"), PlayerPrefs.GetFloat("savedPointY"), 0);
        GameObject.FindGameObjectWithTag("MoveVertical").transform.localPosition = new Vector3(PlayerPrefs.GetFloat("savedCameraX"), PlayerPrefs.GetFloat("savedCameraY"), 0);
        FindObjectOfType<Player_Health>().transform.localPosition = new Vector3(PlayerPrefs.GetFloat("savedPlayerX"), PlayerPrefs.GetFloat("savedPlayerY"), 0);

        FindObjectOfType<StageMovement_Vertical>().GetCurrentPosition();
    }
}
