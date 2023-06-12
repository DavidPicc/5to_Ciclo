using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSituations_Tutorial : MonoBehaviour
{
    public void VulnerableFriendsAndYou()
    {
        FriendScript[] friends = GameObject.FindObjectsOfType<FriendScript>();
        foreach (FriendScript friend in friends)
        {
            friend.canDie = true;
        }
        Player_Health player_Health = GameObject.FindObjectOfType<Player_Health>();
        player_Health.canDie = true;
        Debug.Log("VULENRABLES PUTAS");
    }

    public void TutorialBoss()
    {
        Debug.Log("BOOOOOSSSSS!");
        TutorialManager.instance.ActivateBoss();
    }
}
