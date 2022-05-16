using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;
    public List<Item> Crafting = new List<Item>();
    public List<Item> Crafted = new List<Item>();
    public int maxCraftingRoom, maxCraftedRoom = 4;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public CraftingUI craftingUI;

    private void Awake()
    {
        Instance = this;
        craftingUI.Init();
    }

    public void AddCraftingItem(Item item)
    {
        Item item_ = Instantiate(item);
        if (Crafting.Count >= maxCraftingRoom)
        {
            Debug.Log("No enough crafting room!");
            return;
        }
        int item_ind = CheckContainsItem(item_.itemName, Crafting);
        if (item_ind == -1)
        {
            item_.itemAmount = 1;
            Crafting.Add(item_);
        }
        else
        {
            Crafting[item_ind].itemAmount += 1;
        }
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public void RemoveCraftingItem(Item item)
    {
        Crafting.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public int CheckMaxAmount()
    {
        return 0;
    }
    public void GenerateCraftedItems()
    {

    }
    
    int CheckContainsItem(string itemName, List<Item> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].itemName == itemName && list[i].itemAmount < list[i].maxStack)
            {
                return i;
            }
        }
        return -1;
    }
}
