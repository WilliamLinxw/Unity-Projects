using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevionGames.UIWidgets;

public class CanvasInit : MonoBehaviour
{
    // This script mainly control the activity of the game objects on the canvas
    public GameObject healthBar;
    public GameObject o2Bar;
    public GameObject lsBar;
    public GameObject wBar;
    public GameObject inventoryUI;
    public GameObject craftingUI;
    public GameObject Help3;
    public GameObject itemsDescriptionPanel;
    public GameObject recipeDescriptionPanel;

    private bool[] barsState = new bool[4];

    void Start()
    {
        // set all the bars and panels to false during start
        healthBar.SetActive(false);
        o2Bar.SetActive(false);
        lsBar.SetActive(false);
        wBar.SetActive(false);

        inventoryUI.SetActive(false);
        craftingUI.SetActive(true);  // guess this is a Unity bug; though it has an Awake function, it is still null and cannot be referred to
        craftingUI.SetActive(false);  // so these two lines are used here

    }

    void Update()
    {
        if (!GlobalControl.Instance.videoPlayed) return;  // no video played if played before
        if (InventoryManager.Instance.totalWeight <= 10 && !Help3.activeSelf)
        {
            wBar.SetActive(false);  // hide weight bar if carrying too less
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

        if (Input.GetButtonDown("ItemInfoPanel"))  // toggle the active state of the item information and recipe information panel
        {
            itemsDescriptionPanel.SetActive(!itemsDescriptionPanel.activeSelf);
        }
        if (Input.GetButtonDown("RecipeInfoPanel"))
        {
            recipeDescriptionPanel.SetActive(!recipeDescriptionPanel.activeSelf);
        }
    }

    public void BarsOn()
    {
        // show all the bars
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
