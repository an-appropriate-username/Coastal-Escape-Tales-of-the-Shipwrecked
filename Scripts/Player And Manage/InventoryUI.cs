using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform ItemsParent;
    public GameObject inventoryUI;
    Inventory inventory;
    InventorySlot[] slots;

    [SerializeField] Rigidbody2D rb; 
    void Start()
    {
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive (!inventoryUI.activeSelf);
            if (inventoryUI.activeSelf){
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }
            else{
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.SetRotation(0);
            }
        }
        
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
