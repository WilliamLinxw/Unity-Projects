using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    InventoryManager inventory;
    CraftingManager crafting;
    InventorySlot[] i_slots;
    CraftingSlot[] c_slots;
    public GameObject craftAmtText;
    public static CraftingUI Instance;
    public int currentAmount = 0;

    public Transform inventoryItemsParent;
    public Transform craftingItemsParent;

    private void Awake() {
        Instance = this;
    }
    void Start()
    {
        Init();
    }
    void Update()
    {
        AmtCheck();
    }

    public void Init()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        inventory.onItemChangedCallback += UpdateInventoryUI;
        crafting = GameObject.Find("CraftingManager").GetComponent<CraftingManager>();
        crafting.onItemChangedCallback += UpdateCraftingUI;
        crafting.onItemChangedCallback += UpdateCraftedUI;

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
    void UpdateCraftedUI()
    {
        
    }

    public void AmtIncrement()
    {
        int curAmt = int.Parse(craftAmtText.GetComponent<Text>().text);
        if (curAmt < CraftingManager.Instance.maxAmount)
        {
            craftAmtText.GetComponent<Text>().text = (curAmt + 1).ToString();
            currentAmount  = curAmt + 1;
        }
    }
    
    public void AmtDecrement()
    {
        int curAmt = int.Parse(craftAmtText.GetComponent<Text>().text);
        if (curAmt >= 1)
        {
            craftAmtText.GetComponent<Text>().text = (curAmt - 1).ToString();
            currentAmount = curAmt - 1;
        }
    }

    private void AmtCheck()
    {
        int curAmt = int.Parse(craftAmtText.GetComponent<Text>().text);
        if (curAmt > CraftingManager.Instance.maxAmount)
        {
            craftAmtText.GetComponent<Text>().text = CraftingManager.Instance.maxAmount.ToString();
            currentAmount = CraftingManager.Instance.maxAmount;
        }
    }
}
