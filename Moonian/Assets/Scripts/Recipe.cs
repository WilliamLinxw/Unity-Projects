using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName="Item/Recipe")]
public class Recipe : ScriptableObject
{
    public List<Item> Source_items;
    public List<int> Source_amount;

    public List<Item> Out_items;
    public List<int> Out_amount;

    public bool requireTable = false;
}
