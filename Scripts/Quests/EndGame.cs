using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject EndGamePanel;

    [Header("Dialogue")]
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            {
                player.StopTimer();
                player.rb.constraints = RigidbodyConstraints2D.FreezeAll;
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                EndGamePanel.SetActive(true);
            }
    }

    public void GetOnBoat()
    {
        FindObjectOfType<AudioManager>().Stop("Theme");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
