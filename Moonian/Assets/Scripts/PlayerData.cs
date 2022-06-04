using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float o2;
    public float ls;
    public int refuel;

    public int[] items;
    public int[] picked;

    public float[] pos;

    public PlayerData(Player player, PlayerProperty pp, int[] it, int rf, int[] pked)
    {
        health = pp.currentHealth;
        o2 = pp.currentO2;
        ls = pp.currentLS;

        items = it;
        picked = pked;

        refuel = rf;

        pos = new float[3];
        pos[0] = player.transform.position.x;
        pos[1] = player.transform.position.y;
        pos[2] = player.transform.position.z;
    }
}
