using UnityEngine;

public class Boar : MonoBehaviour
{

    public PlayerMovement player;
    private Rigidbody2D rb;

    private BoxCollider2D boarCollider;

    [Header("Dialogue")]
    public Dialogue dialogue;
    public Dialogue withTruffles;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        boarCollider = GetComponent<BoxCollider2D>();
        //player = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            {
                if(player.quest.title == "Hungry Boar")
                {
                    if(player.quest.goal.currentAmount == player.quest.goal.requiredAmount)
                    {
                        FindObjectOfType<DialogueManager>().StartDialogue(withTruffles);
                        if (player.quest.item != null){Inventory.instance.Remove(player.quest.itemDelete);}
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        boarCollider.enabled = false;
                        rb.velocity = new Vector2(0, 3);
                    }
                    else
                    {
                        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                    }
                }
                else
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
            }
    }
}
