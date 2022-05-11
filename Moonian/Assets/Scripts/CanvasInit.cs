using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInit : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject o2Bar;
    public GameObject inventoryUI;
    public GameObject craftingUI;

    void Start()
    {
        healthBar.SetActive(false);
        o2Bar.SetActive(false);

        inventoryUI.SetActive(false);
        craftingUI.SetActive(false);
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerProperty>().currentHealth < GameObject.Find("Player").GetComponent<PlayerProperty>().maxHealth)
        {
            healthBar.SetActive(true);
        }
        if (GameObject.Find("Player").GetComponent<PlayerProperty>().currentO2 < GameObject.Find("Player").GetComponent<PlayerProperty>().maxO2)
        {
            o2Bar.SetActive(true);
        }
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (inventoryUI.activeSelf)
        {
            craftingUI.SetActive(false);
        }
        if (Input.GetButtonDown("Crafting"))
        {
            craftingUI.SetActive(!craftingUI.activeSelf);
        }
        if (craftingUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }
    }
}
