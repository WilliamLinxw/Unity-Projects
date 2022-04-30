using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;

    public bool Interact()
    {
        if (item.isInteratable)
        {
            if (item.isCollectable)
            {
                InventoryManager.Instance.Add(item);
                return true;
            }
        }
        return false;
    }
}
