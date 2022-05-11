using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    InventoryManager inventory;
    InventorySlot[] slots;
    
    public Transform inventoryItemsParent;
    public Transform craftingItemsParent;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        inventory = InventoryManager.Instance;
        inventory.onItemChangedCallback += UpdateInventoryUI;

        slots = inventoryItemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Items.Count)
            {
                slots[i].AddItem(inventory.Items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
