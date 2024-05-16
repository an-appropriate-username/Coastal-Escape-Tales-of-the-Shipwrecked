
public class ChickenPickUp : Interactable
{

    public Item item;
    public PlayerMovement player;
    public override void Interact()
    {
        if(player.quest.isActive == true)
        {
            PickUp();
        }
    }

    void PickUp(){
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            player.quest.goal.ItemCollected();
            Destroy(gameObject);
        }
    }

}
