using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public GameObject bossDeathTutorialTransition;
    public GameObject boss;

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
        boss.SetActive(true);
    }

    public void VulnerableFriendsAndYou()
    {
        FriendScript[] friends = GameObject.FindObjectsOfType<FriendScript>();
        foreach (FriendScript friend in friends)
        {
            friend.canDie = true;
        }
        Player_Health player_Health = GameObject.FindObjectOfType<Player_Health>();
        player_Health.canDie = true;
    }
}
