using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// to store all the collectable items (for the convenience of the store and load system)
[System.Serializable]
public class ItemList: MonoBehaviour
{
    public List<Item> AllItems;
}
