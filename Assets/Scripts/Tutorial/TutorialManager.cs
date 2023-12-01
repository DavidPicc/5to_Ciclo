using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private bool estaRalentizado1 = false;
    private bool estaRalentizado2 = false;
    private bool estaRalentizado3 = false;

    private float velocidadNormal = 1.0f;
    private float velocidadRalentizada = 0f;

    public static TutorialManager instance;

    public GameObject boss;

    public GameObject moveKeys, shootKey, abilityKey;
    public float timerKeys;

    public bool canMove, canShoot, canAbility = false;

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
        boss = GameObject.FindGameObjectWithTag("bossTutorial");

        if(boss) boss.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FindObjectOfType<Player_Movement>().enabled = false;
        FindObjectOfType<Player_Shoot>().enabled = false;
        FindObjectOfType<Player_Ability2>().enabled = false;
    }

    private void Update()
    {
        if(estaRalentizado1 == true)
        {
            HideKeys1();
        }

        if (estaRalentizado2 == true)
        {
            HideKeys2();
        }

        if (estaRalentizado3 == true)
        {
            HideKeys3();
        }
    }
    public void ActivateBoss()
    {
        Debug.Log("BOOOOOSSSSS!");
        boss.SetActive(true);
    }

    public void VulnerableFriendsAndYou()
    {
        FindObjectOfType<BossT_Shoot>().startShooting = true;

        FriendScript[] friends = GameObject.FindObjectsOfType<FriendScript>();
        foreach (FriendScript friend in friends)
        {
            friend.canDie = true;
        }
        Player_Health player_Health = GameObject.FindObjectOfType<Player_Health>();
        player_Health.canDie = true;
        Debug.Log("VULNERABLE");
    }

    public void ShowMove()
    {
        canMove = true;
        FindObjectOfType<Player_Movement>().enabled = true;
        FindObjectOfType<Player_Movement>().gameObject.GetComponent<Animator>().enabled = false;
        moveKeys.SetActive(true);
        estaRalentizado1 = true;
        Time.timeScale = velocidadRalentizada; 
    }

    public void ShowShoot()
    {
        canShoot = true;
        FindObjectOfType<Player_Shoot>().enabled = true;
        shootKey.SetActive(true);
        estaRalentizado2 = true;
        Time.timeScale = velocidadRalentizada;
    }

    public void ShowAbility()
    {
        canAbility = true;
        FindObjectOfType<Player_Ability2>().enabled = true;
        abilityKey.SetActive(true);
        estaRalentizado3 = true;
        Time.timeScale = velocidadRalentizada;
    }

    void HideKeys1()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveKeys.SetActive(false);
            Time.timeScale = velocidadNormal;
            estaRalentizado1 = false;
        }
    }

    void HideKeys2()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            shootKey.SetActive(false);
            Time.timeScale = velocidadNormal;
            estaRalentizado2 = false;
        }
    }

    void HideKeys3()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            abilityKey.SetActive(false);
            Time.timeScale = velocidadNormal;
            estaRalentizado3 = false;
        }
    }
}
