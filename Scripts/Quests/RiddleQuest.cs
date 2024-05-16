using UnityEngine;
using TMPro;

public class RiddleQuest : Interactable
{
    //Dialogue Variables

    [Header("Dialogue")]
    public Dialogue dialogue;

    public Dialogue afterQuest;

    public Dialogue fortune;

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

    //Dialogue Functions

    public override void Interact()
    {
        if(quest.isComplete == false)
        {
            if (dialougueOff)
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialougueOff = true;
            player.quest = quest;
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

    public void CloseQuestWindow()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        player.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        questWindow.SetActive(false);
        buttonWindow.SetActive(false);
        FindObjectOfType<DialogueManager>().EndDialogue();
    }

    public void IncorrectAnswer()
    {
        if (quest.isComplete == false)
        {
            FindObjectOfType<AudioManager>().Play("Click");
            FindObjectOfType<AudioManager>().Play("Negative");
            descriptionText.text = "Well.. I expected more from you..";
            quest.Complete();
            acornText.text = "0";
            rewardAmount.text = "0";
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
        else{
            FindObjectOfType<AudioManager>().Play("Click");
        }
    }

    public void CorrectAnswer()
    {
        if (quest.isComplete == false)
        {
            FindObjectOfType<AudioManager>().Play("Click");
            FindObjectOfType<AudioManager>().Play("AcceptQuest");
            descriptionText.text = "Very impressive! Your reward, as promised.";
            player.HandInQuest();
            TellFortune();
        }
        else{
            FindObjectOfType<AudioManager>().Play("Click");
        }
    }

    public void TellFortune()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(fortune);
    }

}

