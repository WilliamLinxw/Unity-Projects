using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this defines the slot of the crafted outcomes
public class CraftedSlot : MonoBehaviour
{
    public Image icon;
    public GameObject numText;
    Item item;

    // add an new item to the slot: set spirite, amount and the displayed amount
    public void AddItem(Item newItem)
    {
        item = Instantiate(newItem);

        icon.sprite = item.icon;
        icon.enabled = true;

        if (item.itemAmount > 1)
        {
            numText.SetActive(true);
        }

        numText.GetComponent<Text>().text = newItem.itemAmount.ToString();
    }

    // reset the slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        numText.SetActive(false);
        numText.GetComponent<Text>().text = 0.ToString();

    }

    // by clicking the item -> add the item to the inventory
    public void OnCollectButton()
    {
        if (item != null)
        {
            int am = item.itemAmount;
            Item item_ = Instantiate(item);
            item_.itemAmount = 1;
            InventoryManager.Instance.Add(item_);
            item.itemAmount -= 1;
            numText.GetComponent<Text>().text = item.itemAmount.ToString();
            foreach (Item it in CraftingManager.Instance.Crafted)
            {
                if (it.id == item.id)
                {
                    it.itemAmount -= 1;
                    break;
                }
            }
            if (item.itemAmount < 1)
            {
                ClearSlot();
            }
        }
    }
}
