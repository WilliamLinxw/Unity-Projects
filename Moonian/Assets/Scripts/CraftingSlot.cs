using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject numText;
    public bool inCrafting = true;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        UpdateAmount();

        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        numText.SetActive(false);
        numText.GetComponent<Text>().text = 0.ToString();

        removeButton.interactable = false;
    }
    public void UpdateAmount()
    {
        if (item != null)
        {
            if (item.itemAmount > 1)
            {
                numText.SetActive(true);
            }
            else
            {
                numText.SetActive(false);
            }
            numText.GetComponent<Text>().text = item.itemAmount.ToString();
        }
    }

    public void OnRemoveButton()
    {
        InventoryManager.Instance.Add(item);
    }
}
