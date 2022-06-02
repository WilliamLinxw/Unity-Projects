using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeRocket : MonoBehaviour
{
    // public static EscapeRocket Instance;
    public float fuel {get { return _fuel;}}
    public GameObject RefuelBar;
    private float _fuel;
    private float maxFuel = 50;

    void Awake()
    {
        // Instance = this;
        _fuel = 0;
    }

    public void Refuel()
    {
        bool flag = false;
        foreach (Item i in InventoryManager.Instance.Items)
        {
            if (i.id == 2)
            {
                flag = true;
                break;
            }
        }
        if (!flag) return;  // no fuel -> cannot refuel
        if (Input.GetButtonDown("Refuel"))
        {
            _fuel += 1;
            if (_fuel >= maxFuel)
            {
                GlobalControl.Instance.Win();
            }
        }
    }

}
