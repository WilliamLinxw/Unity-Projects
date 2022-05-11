using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;
    public List<Item> Crafting = new List<Item>();
    public List<Item> Crafted = new List<Item>();
    public int maxCraftingRoom, maxCraftedRoom = 4;

    public CraftingUI craftingUI;

    private void Awake()
    {
        Instance = this;
        craftingUI.Init();
    }

    public void AddCraftingItem(Item item)
    {
        if (Crafting.Count > maxCraftingRoom)
        {
            return;
        }
        if (Crafting.Contains(item))
        {
            Crafting[Crafting.IndexOf(item)].itemAmount += 1;
        }
        else
        {
            item.itemAmount = 1;
            Crafting.Add(item);
        }
    }
    public void GenerateCraftedItems()
    {

    }
}
