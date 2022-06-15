using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// defines the consumable items, based on the base class Item
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public float itemValue;
    public bool isUpTo;  // two types of consumable -> recover some properties to some value or recover it by some value

    public override void Use()
    {
        switch (category)  // distinct categories
        {
            case ItemCategory.O2:
                PlayerProperty.Instance.SupplyO2(itemValue);
                break;
            
            case ItemCategory.Medicine:
                switch (isUpTo)
                {
                    case false:
                        PlayerProperty.Instance.Cure(itemValue);
                        break;
                    case true:
                        PlayerProperty.Instance.CureUpTo(itemValue);
                        break;
                }
                break;
            case ItemCategory.Food:
            case ItemCategory.Water:
                PlayerProperty.Instance.Cure(itemValue);
                break;
            case ItemCategory.LSSupplement:
                if (isUpTo)
                {
                    PlayerProperty.Instance.SupplyLSUpTo(itemValue);
                }
                else
                {
                    PlayerProperty.Instance.SupplyLS(itemValue);
                }
                break;
            default:
                break;
        }
    }
}
