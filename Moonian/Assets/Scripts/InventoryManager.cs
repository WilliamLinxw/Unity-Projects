using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public int maxRoom = 40;

    private float totalWeight = 0;
    private float maxWeight = 180;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public InventoryUI inventoryUI;

    private void Awake()
    {
        Instance = this;
        inventoryUI.Init();
    }
    public void Add(Item item)
    {
        if (Items.Count > maxRoom)
        {
            Debug.Log("No enough room!");
            // TODO alerts required
            return;
        }
        else if (totalWeight > maxWeight)
        {
            Debug.Log("Overweight!");
            // TODO some measures later; currently nothing would happen
        }
        Items.Add(item);
        float newWeight = totalWeight + item.weight;
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public void Remove(Item item)
    {
        Items.Remove(item);
        totalWeight -= item.weight;
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public void Use(Item item)
    {
        item.Use();
        Remove(item);
        // use switch
    }

}
