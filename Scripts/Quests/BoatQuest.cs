using UnityEngine;
using TMPro;

public class BoatQuest : Interactable
{
    //Dialogue Variables

    [Header("Dialogue")]
    public Dialogue dialogue;

    public Dialogue afterQuest;

    //Dialogue booleans
    private bool dialougueOff;

    //Quest Variables 
    [Header("Quest")]
    public Quest quest;

    [Header("Variables")]
    public PlayerMovement player;
    public GameObject questWindow;
    public GameObject buttonWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI acornText;
    public TextMeshProUGUI rewardAmount;

    //End Game variables

    public GameObject endGameTrigger;
    public bool acornsDelivered;

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
        else if (quest.isComplete == true)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(afterQuest);
            dialougueOff = true;
        }
    }

    private void Start()
    {
        dialougueOff = true;
        acornsDelivered = false;
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
            rewardAmount.text = quest.rewardAmount.ToString();
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
            player.quest.goal.currentAmount = player.acornWealth;
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
            player.quest.goal.currentAmount = player.acornWealth;
            if (player.quest.goal.IsReached())
        {
            descriptionText.text = "Well, a fair trade I'd say! Just jump on the boat and set sail whenever you're ready.";
            player.acornWealth -= 200;
            acornsDelivered = true;
            endGameTrigger.SetActive(true);
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
