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
        FindObjectOfType<AudioManager>().Play("Click");
        if(quest.isComplete == false)
        {
            FindObjectOfType<AudioManager>().Play("AcceptQuest");
            quest.isActive = true;
            player.quest = quest;
        }
        CloseQuestWindow();
    }

    public void CloseQuestWindow()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        player.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        questWindow.SetActive(false);
        buttonWindow.SetActive(false);
        FindObjectOfType<DialogueManager>().EndDialogue();
    }

    public void CheckComplete()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        if(player.quest.isActive == true)
        {
            if (player.quest.goal.IsReached())
        {
            descriptionText.text = "Thankyou for helping! Heres your reward.";
            player.HandInQuest();
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Negative");
            Debug.Log("QUEST NOT COMPLETE");
        }
        }
        if (player.quest.isActive == false){FindObjectOfType<AudioManager>().Play("Negative");}
    }
}
