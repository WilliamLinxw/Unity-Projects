using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public float itemValue;
    public bool isUpTo;
    // PlayerProperty playerProperty;

    public override void Use()
    {
        switch (category)
        {
            case ItemCategory.O2:
                PlayerProperty.Instance.SupplyO2(itemValue);
                break;
            
            case ItemCategory.Medicine:
                switch (isUpTo)
                {
                    case true:
                        PlayerProperty.Instance.Cure(itemValue);
                        break;
                    case false:
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
