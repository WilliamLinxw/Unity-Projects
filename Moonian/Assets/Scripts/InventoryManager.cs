using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public int maxRoom = 40;
    public float totalWeight
    {
        get { return _totalWeight;}
    }

    private float _totalWeight = 0;
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
        if (item.itemAmount == 0)
        {
            item.itemAmount = 1;
        }
        if (Items.Count >= maxRoom)
        {
            Debug.Log("No enough room!");
            // TODO alerts required
            return;
        }
        else if (_totalWeight > maxWeight)
        {
            Debug.Log("Overweight!");
            PlayerProperty.Instance.isOverweight = true;
        }
        else
        {
            PlayerProperty.Instance.isOverweight = false;
        }
        bool itemplaced = false;
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].id == item.id)
            {
                Items[i].itemAmount += item.itemAmount;
                itemplaced = true;
                break;
            }
        }
        if (!itemplaced)
        {
            Items.Add(Instantiate(item));
        }    
        
        _totalWeight += item.weight;
        // Debug.Log(totalWeight);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public void Remove(Item item)
    {
        Items.Remove(item);
        _totalWeight -= item.weight * item.itemAmount;
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public void Use(Item item, bool inCrafting)
    {
        if (!inCrafting && item.isInteratable && (item is Consumable))
        {
            Items[Items.IndexOf(item)].itemAmount -= 1;
            item.Use();
        }
        else if (inCrafting && item.isCollectable)
        {
            Items[Items.IndexOf(item)].itemAmount -= 1;

            CraftingManager.Instance.AddCraftingItem(item);
        }
        if (Items[Items.IndexOf(item)].itemAmount < 1)
        {
            Remove(item);
        }
        _totalWeight -= item.weight;
    }

    public void CalcWeight()
    {
        float w = 0;
        foreach (Item i in Items)
        {
            w += i.itemAmount * i.weight;
        }
        _totalWeight = w;
    }

}
