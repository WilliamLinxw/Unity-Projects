using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;
    public List<Item> Crafting = new List<Item>();
    public List<Item> Crafted = new List<Item>();
    public int maxCraftingRoom = 4;
    public int maxCraftedRoom = 4;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    // public CraftingUI craftingUI;

    public List<Item> SortedCrafting;
    private List<Recipe> Recipes;
    public int maxAmount = 0;
    public int currentAmount = 0;
    private Recipe rcp = null;


    private void Awake()
    {
        Instance = this;
        // craftingUI.Init();
        Recipes = GetComponent<CraftingRecipes>().Recipes;
    }

    private void Update() 
    {
        currentAmount = CraftingUI.Instance.currentAmount;
        Crafting = Remove0Items(Crafting);
        Crafted = Remove0Items(Crafted);
    }

    public void AddCraftingItem(Item item)
    {
        Item item_ = Instantiate(item);
        int item_ind = CheckContainsItem(item_.itemName, Crafting);
        if (Crafting.Count >= maxCraftingRoom && item_ind == -1)
        {
            Debug.Log("No enough crafting room!");
            item_.itemAmount = 1;
            InventoryManager.Instance.Add(item_);
            return;
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
            CheckRecipes();
            onItemChangedCallback.Invoke();
        }
    }
    public void RemoveCraftingItem(Item item)
    {
        if (item == null)
        {
            return;
        }
        // Debug.Log(item.itemAmount);
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
            CheckRecipes();
            onItemChangedCallback.Invoke();
        }
    }

    public void CheckRecipes()
    {
        maxAmount = 0;
        SortedCrafting = new List<Item>(Crafting);
        SortedCrafting.Sort(SortFunc);
        foreach (Recipe r in Recipes)
        {
            if (r.Source_items.Count != SortedCrafting.Count)
            {
                continue;
            }
            int checkFlag = 0;
            for (int i = 0; i < r.Source_items.Count; i++)
            {
                if (r.Source_items[i].id == SortedCrafting[i].id)
                {
                    checkFlag += 1;
                }
            }
            if (checkFlag == r.Source_items.Count)
            {
                // a recipe is found => then check the amounts
                Debug.Log("Current recipe: " + r.name);
                rcp = r;
                List<int> craftingAmount = new List<int>();
                foreach (Item it in SortedCrafting)
                {
                    craftingAmount.Add(it.itemAmount);
                }
                maxAmount = AmountListComp(r.Source_amount, craftingAmount);
                // Debug.Log(maxAmount);
                maxAmount = Mathf.Max(0, maxAmount);
                break;
            }
        }
    }

    public void GenerateCraftedItems()
    {
        if (rcp != null)
        {
            if (rcp.requireTable && !Player.Instance.atWorktable)
            {
                return;  // at worktable not satisfied -> directly return
            }
            for (int i = 0; i < rcp.Out_items.Count; i++)
            {
                int flag = 0;
                foreach (Item c in Crafted)
                {
                    if (c.id == rcp.Out_items[i].id)
                    {
                        c.itemAmount += rcp.Out_amount[i] * currentAmount;
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    Crafted.Add(Instantiate(rcp.Out_items[i]));
                    Crafted[i].itemAmount = rcp.Out_amount[i] * currentAmount;
                }
            }
            AddCraftedItems();
            // then remove the required crafting items
            for (int j = 0; j < rcp.Source_items.Count; j++)
            {
                for (int k = 0; k < Crafting.Count; k++)
                {
                    if (Crafting[k].id == rcp.Source_items[j].id)
                    {
                        Crafting[k].itemAmount -= rcp.Source_amount[j] * currentAmount;
                        if (Crafting[k].itemAmount == 0)
                        {
                            Crafting.RemoveAt(k);
                        }
                        CraftingUI.Instance.UpdateCraftingUI();
                        break;
                    }
                }
            }
            rcp = null;
            CheckRecipes();
            CraftingUI.Instance.AmtCheck();
        }

    }
    public void AddCraftedItems()
    {
        CraftingUI.Instance.UpdateCraftedUI();
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

    public void DropItems()
    {
        // clear the crafting items
        Crafting = new List<Item>();
        SortedCrafting = new List<Item>();
        Crafted = new List<Item>();
        maxAmount = 0;

        CraftingUI.Instance.UpdateCraftingUI();
        CraftingUI.Instance.UpdateCraftedUI();
    }

    private int SortFunc(Item a, Item b)
    {
        if (a.id < b.id)
            return -1;
        else if (a.id > b.id)
            return 1;
        return 0;
    }

    private int AmountListComp(List<int> a, List<int> b)
    {
        if (a.Count != b.Count) return -1;
        else
        {
            int amt = 255;
            for (int i = 0; i < a.Count; i++)
            {
                amt = Mathf.Min(amt, (int)b[i]/a[i]);
            }
            if (amt < 1) return 0;
            else return amt;
        }
    }

    private List<Item> Remove0Items(List<Item> l)
    {
        foreach (Item i in l)
        {
            if (i == null || i.itemAmount <= 0)
            {
                l.Remove(i);
            }
        }
        return l;
    }
}
