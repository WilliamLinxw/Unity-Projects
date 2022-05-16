using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    InventoryManager inventory;
    CraftingManager crafting;
    InventorySlot[] i_slots;
    CraftingSlot[] c_slots;

    
    public Transform inventoryItemsParent;
    public Transform craftingItemsParent;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        inventory.onItemChangedCallback += UpdateInventoryUI;
        crafting = GameObject.Find("CraftingManager").GetComponent<CraftingManager>();
        crafting.onItemChangedCallback += UpdateCraftingUI;

        i_slots = inventoryItemsParent.GetComponentsInChildren<InventorySlot>();
        c_slots = craftingItemsParent.GetComponentsInChildren<CraftingSlot>();
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < i_slots.Length; i++)
        {
            if (i < inventory.Items.Count)
            {
                i_slots[i].AddItem(inventory.Items[i]);
            }
            else
            {
                i_slots[i].ClearSlot();
            }
        }
    }
    void UpdateCraftingUI()
    {
        for (int i = 0; i < c_slots.Length; i++)
        {
            if (i < crafting.Crafting.Count)
            {
                c_slots[i].AddItem(crafting.Crafting[i]);
            }
            else
            {
                c_slots[i].ClearSlot();
            }
        }
    }
}
