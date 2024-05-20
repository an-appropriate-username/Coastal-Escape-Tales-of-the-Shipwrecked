using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : Interactable
{
    public Dialogue dialogue;

    //Dialogue booleans
    private bool dialougueOff;
    public override void Interact()
    {
        if (dialougueOff)
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialougueOff = true;
    }

    private void Start()
    {
        dialougueOff = true;
    }
}
