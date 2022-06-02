using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    InventoryManager inventory;
    InventorySlot[] slots;
    void Start()
    {
        Init();
    }

    public void Init()
    {
        inventory = InventoryManager.Instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateUI()
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
