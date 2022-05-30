using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftedSlot : MonoBehaviour
{
    public Image icon;
    public GameObject numText;
    Item item;

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

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        numText.SetActive(false);
        numText.GetComponent<Text>().text = 0.ToString();

    }

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
