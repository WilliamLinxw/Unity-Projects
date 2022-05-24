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
    public List<Item> SortedCrafting;

    private void Awake()
    {
        Instance = this;
        craftingUI.Init();
    }

    public void AddCraftingItem(Item item)
    {
        Item item_ = Instantiate(item);
        int item_ind = CheckContainsItem(item_.itemName, Crafting);
        if (Crafting.Count >= maxCraftingRoom && item_ind != -1)
        {
            Debug.Log("No enough crafting room!");
            item_.itemAmount = 1;
            InventoryManager.Instance.Add(item_);
        }
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
            CheckMaxAmount();
            onItemChangedCallback.Invoke();
        }
    }
    public void RemoveCraftingItem(Item item)
    {
        if (item == null)
        {
            return;
        }
        Debug.Log(item.itemAmount);
        for (int i = 0; i < Crafting.Count; i++)
        {
            if (Crafting[i] != null && item.id == Crafting[i].id)
            {
                Debug.Log(Crafting[i].itemAmount);
                Crafting.RemoveAt(i);
                break;
            }
        }
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public int CheckRecipes()
    {
        SortedCrafting = new List<Item>(Crafting);
        SortedCrafting.Sort(SortFunc);
        return 0;
    }
    public int CheckMaxAmount()
    {
        CheckRecipes();
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

    private int SortFunc(Item a, Item b)
    {
        if (a.id < b.id)
            return -1;
        else if (a.id > b.id)
            return 1;
        return 0;
    }
}
