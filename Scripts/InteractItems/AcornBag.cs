using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AcornBag : Interactable
{
    public Sprite open; //Already Opened, could also mean "Closed"
    public Sprite closed; //Is not yet interacted with! 

    public PlayerMovement player;
    private SpriteRenderer sr;
    private bool whetherOpen;

    public override void Interact()
    {
        if(whetherOpen == false)
        {
            sr.sprite = open;
            player.acornWealth += 15;
            whetherOpen = true;
        }
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
        whetherOpen = false;
    }
}
