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
    public static TutorialManager instance;
    //public GameObject bossDeathTutorialTransition;
    public GameObject boss;

    public GameObject moveKeys, shootKey, abilityKey;
    public float timerKeys;

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
        //bossDeathTutorialTransition = GameObject.FindGameObjectWithTag("transitionLevel");
        boss = GameObject.FindGameObjectWithTag("bossTutorial");

        //if(bossDeathTutorialTransition != null) bossDeathTutorialTransition.SetActive(false);
        if(boss) boss.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
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
        moveKeys.SetActive(true);
        Invoke("HideKeys", timerKeys);
    }

    public void ShowShoot()
    {
        shootKey.SetActive(true);
        Invoke("HideKeys", timerKeys);
    }

    public void ShowAbility()
    {
        abilityKey.SetActive(true);
        Invoke("HideKeys", timerKeys);
    }

    void HideKeys()
    {
        moveKeys.SetActive(false);
        shootKey.SetActive(false);
        abilityKey.SetActive(false);
    }
}
