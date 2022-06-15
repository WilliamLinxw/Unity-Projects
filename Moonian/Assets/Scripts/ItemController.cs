using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attached to each prefabs -> so that the item-related operations can be performed
public class ItemController : MonoBehaviour
{
    public Item item;

    public bool Interact()
    {
        if (item.isInteratable)
        {
            if (item.id == 256)
            {
                Debug.Log("Collecting a container");
                // for a container
                for (int i = 0; i < GetComponentInParent<BoxOfItems>().Contents.Count; i++)
                {
                    Item item_ = GetComponentInParent<BoxOfItems>().Contents[i];
                    item_.itemAmount = 1;
                    int am = GetComponentInParent<BoxOfItems>().Amounts[i];
                    for (int j = 0; j< am; j++)
                    {
                        InventoryManager.Instance.Add(item_);
                    }
                }
                return true;
            }
            if (item.isCollectable)
            {
                InventoryManager.Instance.Add(item);
                return true;
            }
        }
        
        return false;
    }
}
