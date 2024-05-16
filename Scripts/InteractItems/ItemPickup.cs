using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;
    public override void Interact()
    {
        PickUp();
    }

    void PickUp(){
        FindObjectOfType<AudioManager>().Play("PickUp");
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }

}
