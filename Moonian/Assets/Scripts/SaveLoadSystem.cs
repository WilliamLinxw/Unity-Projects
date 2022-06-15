using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls the save and load of the game process
// Ref: https://www.youtube.com/watch?v=XOjd_qU2Ido
public class SaveLoadSystem : MonoBehaviour
{
    public List<Item> iList;
    public EscapeRocket escapeRocket;
    public GameObject deathMenu;

    public void SavePlayer()
    {
        if (!CheckNull(Player.Instance, PlayerProperty.Instance, InventoryManager.Instance)) return;
        int[] it = new int[iList.Count];
        foreach (Item i in InventoryManager.Instance.Items)
        {
            for (int j = 0; j < iList.Count; j++)
            {
                if (i.id == iList[j].id)
                {
                    it[j] = i.itemAmount;
                    break;
                }
            }
        }
        int rf = escapeRocket.fuel;
        int[] picked = InventoryManager.Instance.picked;
        SaveNLoad.SavePlayer(Player.Instance, PlayerProperty.Instance, it, rf, picked);
    }

    public void LoadPlayer()
    {
        if (!CheckNull(Player.Instance, PlayerProperty.Instance, InventoryManager.Instance)) return;  // so that these instances exist
        PlayerData data = SaveNLoad.LoadPlayer();

        PlayerProperty.Instance.SetProp(data.health, data.o2, data.ls);

        Vector3 position;
        position.x = data.pos[0];
        position.y = data.pos[1];
        position.z = data.pos[2];
        Player.Instance.SetPos(position);

        InventoryManager.Instance.Items = new List<Item>();

        for (int j = 0; j < iList.Count; j++)
        {
            if (data.items[j] == 0) continue;
            Item i = Instantiate(iList[j]);
            i.itemAmount = data.items[j];
            InventoryManager.Instance.Items.Add(Instantiate(i));
            Destroy(i);
        }
        InventoryManager.Instance.inventoryUI.UpdateUI();
        InventoryManager.Instance.CalcWeight();

        escapeRocket.LoadSetFuel(data.refuel);
        InventoryManager.Instance.picked = data.picked;

        Time.timeScale = 1f;

        if (deathMenu.activeSelf)
        {
            deathMenu.SetActive(false);
        }
    }

    private bool CheckNull(Player p, PlayerProperty pp, InventoryManager im)
    {
        if(p != null && pp != null && im != null) return true;
        else return false;
    }
}
