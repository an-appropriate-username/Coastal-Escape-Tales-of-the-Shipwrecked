using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    //GENERAL VARIABLES 
    public Image icon;
    public Button removeButton;
    Item item;
    public PlayerMovement player;

    //ITEMS

    public int peachRecharge = 20;

    public void AddItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;    
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void onRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            if (item.name == "Peach")
            {
                EatPeach();
            }
        }
    }

    public void EatPeach()
    {
        player.hunger += peachRecharge;
        if (player.hunger > player.maxHunger) player.hunger = player.maxHunger;
        onRemoveButton();
    }
}
