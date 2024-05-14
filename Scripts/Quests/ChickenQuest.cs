using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChickenQuest : MonoBehaviour
{
    public Quest quest;
    public PlayerMovement player;
    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI acornText;

    public TextMeshProUGUI rewardAmount;

    public void OpenQuestWindow()
    {
        if(quest.isComplete == false)
        {
            questWindow.SetActive(true);
            titleText.text = quest.title;
            descriptionText.text = quest.description;
            acornText.text = quest.acornReward.ToString();
        }
    }

    public void AcceptQuest()
    {
        if(quest.isComplete == false)
        {
            quest.isActive = true;
            player.quest = quest;
        }
        questWindow.SetActive(false);
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }

    public void CheckComplete()
    {
        if(player.quest.isActive == true)
        {
            if (player.quest.goal.IsReached())
        {
            player.HandInQuest();
        }
        else
        {
            Debug.Log("QUEST NOT COMPLETE");
        }
        }
    }
}
