using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Openable : Interactable
{
    public Sprite open; //Already Opened, could also mean "Closed"
    public Sprite closed; //Is not yet interacted with! 

    private SpriteRenderer sr;
    private bool whetherOpen;

    public override void Interact()
    {
        if(whetherOpen)
            sr.sprite = open;
        else
            sr.sprite = open; 

        whetherOpen = !whetherOpen;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
    }
}
