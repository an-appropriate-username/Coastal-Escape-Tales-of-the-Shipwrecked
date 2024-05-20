using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroNPC : MonoBehaviour
{
    public PlayerMovement player;

    [Header("Dialogue")]
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
    }
}
