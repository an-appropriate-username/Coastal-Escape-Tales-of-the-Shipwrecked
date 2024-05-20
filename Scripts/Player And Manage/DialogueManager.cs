using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;

    public PlayerMovement player;
    private Queue<string> sentences;
    
    [SerializeField] Rigidbody2D rb; 

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {   
        sentences.Enqueue(sentence);
        } 

        DisplayNextSentence(); 
    }

    public void DisplayNextSentence()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        if (sentences.Count == 0)
        {
            EndDialogue();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

}
