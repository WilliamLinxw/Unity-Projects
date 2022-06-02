using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInit : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject o2Bar;
    public GameObject lsBar;
    public GameObject wBar;
    public GameObject inventoryUI;
    public GameObject craftingUI;
    public GameObject Help3;

    private bool[] barsState = new bool[4];

    void Start()
    {
        healthBar.SetActive(false);
        o2Bar.SetActive(false);
        wBar.SetActive(false);

        inventoryUI.SetActive(false);
        craftingUI.SetActive(true);
        craftingUI.SetActive(false);
    }

    void Update()
    {
        if (InventoryManager.Instance.totalWeight == 0 && !Help3.activeSelf)
        {
            wBar.SetActive(false);  // hide weight bar if carrying no items
        }
        else wBar.SetActive(true);
        if (GameObject.Find("Player").GetComponent<PlayerProperty>().currentHealth < GameObject.Find("Player").GetComponent<PlayerProperty>().maxHealth)
        {
            healthBar.SetActive(true);  // show health bar when health < max health; same for the oxygen level and life support module below
        }
        if (GameObject.Find("Player").GetComponent<PlayerProperty>().currentO2 < GameObject.Find("Player").GetComponent<PlayerProperty>().maxO2)
        {
            o2Bar.SetActive(true);
        }
        else o2Bar.SetActive(false);
        if (GameObject.Find("Player").GetComponent<PlayerProperty>().currentLS < GameObject.Find("Player").GetComponent<PlayerProperty>().maxLS)
        {
            lsBar.SetActive(true);
        }
        else lsBar.SetActive(false);
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);  // toggle inventory UI and crafting UI
        }
        if (inventoryUI.activeSelf)
        {
            craftingUI.SetActive(false);  // Inactive the crafting panel when showing the inventory panel. Same below
        }
        if (Input.GetButtonDown("Crafting"))
        {
            if (craftingUI.activeSelf)
            {
                float drop = Random.value;
                if (drop <= 0.5) CraftingManager.Instance.DropItems();  // penalty: there's half chance of losing what is "currently stored" in the crafting panel!
            }
            craftingUI.SetActive(!craftingUI.activeSelf);
        }
        if (craftingUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }
    }

    public void BarsOn()
    {
        barsState[0] = healthBar.activeSelf;
        barsState[1] = o2Bar.activeSelf;
        barsState[2] = lsBar.activeSelf;
        barsState[3] = wBar.activeSelf;

        healthBar.SetActive(true);
        o2Bar.SetActive(true);
        lsBar.SetActive(true);
        wBar.SetActive(true);
    }

    public void BarsBack()
    {
        // instead of setting them off, we set them to the initial state
        healthBar.SetActive(barsState[0]);
        o2Bar.SetActive(barsState[1]);
        lsBar.SetActive(barsState[2]);
        wBar.SetActive(barsState[3]);
    }
}
