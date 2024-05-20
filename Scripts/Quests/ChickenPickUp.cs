
public class ChickenPickUp : Interactable
{

    public Item item;
    public PlayerMovement player;

    public string questName = "Hen From The Brood";
    public override void Interact()
    {
        if (player.quest.title == questName)
        {
            if(player.quest.isActive == true)
            {
                PickUp();
            } 
        }
    }

    void PickUp(){
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            FindObjectOfType<AudioManager>().Play("PickUp");
            player.quest.goal.ItemCollected();
            Destroy(gameObject);
        }
    }

}
