using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChickenQuest : Interactable
{
    //Dialogue Variables

    public Dialogue dialogue;

    //Dialogue booleans
    private bool dialougueOff;

    //Quest Variables 
    public Quest quest;
    public PlayerMovement player;
    public GameObject questWindow;
    public GameObject buttonWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI acornText;
    public TextMeshProUGUI rewardAmount;

    //Dialogue Functions

    public override void Interact()
    {
        if(quest.isComplete == false)
        {
            if (dialougueOff)
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialougueOff = true;
            OpenQuestWindow();
        }
    }

    private void Start()
    {
        dialougueOff = true;
    }

    //Quest Functions

    public void OpenQuestWindow()
    {
        if(quest.isComplete == false)
        {
            questWindow.SetActive(true);
            buttonWindow.SetActive(true);
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
        CloseQuestWindow();
    }

    public void CloseQuestWindow()
    {
        player.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        questWindow.SetActive(false);
        buttonWindow.SetActive(false);
        FindObjectOfType<DialogueManager>().EndDialogue();
    }

    public void CheckComplete()
    {
        if(player.quest.isActive == true)
        {
            if (player.quest.goal.IsReached())
        {
            descriptionText.text = "Thankyou for helping! Heres your reward.";
            player.HandInQuest();
        }
        else
        {
            Debug.Log("QUEST NOT COMPLETE");
        }
        }
    }
}
