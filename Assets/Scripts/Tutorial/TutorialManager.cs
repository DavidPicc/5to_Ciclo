using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public GameObject bossDeathTutorialTransition;
    public GameObject boss;

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
        bossDeathTutorialTransition = GameObject.FindGameObjectWithTag("transitionLevel");
        boss = GameObject.FindGameObjectWithTag("bossTutorial");

        if(bossDeathTutorialTransition != null) bossDeathTutorialTransition.SetActive(false);
        if(boss) boss.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        bossDeathTutorialTransition.SetActive(false);
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

    public void FinishedTutorial()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player_Health>().enabled = false;
        player.GetComponent<Player_Movement>().enabled = false;
        player.GetComponent<Player_Shoot>().enabled = false;
        bossDeathTutorialTransition.SetActive(true);
        Debug.Log("NIVEL 1 DESBLOQUEADO");
    }
}
