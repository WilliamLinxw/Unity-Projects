using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the script for the containers -> to define what is contained
public class BoxOfItems : MonoBehaviour
{
    public List<Item> Contents = new List<Item>();
    public List<int> Amounts = new List<int>();
}
