using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName="Item/Common Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public bool isInteratable = true;
    public bool isCollectable;
    public bool isSpawnable;
    public float weight;
    public int itemAmount;  // use this for stack
    public int maxStack = 100;

    public ItemCategory category;

    public virtual void Use()
    {
        // virtual: so that this function can be specified for items
        // use this item...
        Debug.Log("Using" + itemName);
    }
}

public enum ItemCategory {Flag, Food, Fuel, LSSupplement, Medicine, O2, Ore, ShipWreckage, TerrainObj, Tools, Water, Others}
