using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeRocket : MonoBehaviour
{
    // this class is to store the refueling state of the escape rocket
    public int fuel {get { return _fuel;}}
    public RefuelBar rBar;
    public Button launch;
    private int _fuel;
    private int maxFuel = 20;

    void Start()
    {
        _fuel = 0;
        launch.interactable = false;
    }
    void Update()
    {
        if (Player.Instance != null)
        {
            // when the player gets nearby the escape rocket, show the bar and launch button
            if (Player.Instance.atRefueling)
            {
                rBar.gameObject.SetActive(true);
                launch.gameObject.SetActive(true);
            }
            else
            {
                rBar.gameObject.SetActive(false);
                launch.gameObject.SetActive(false);
            }
        }
    }

    public void Refuel()
    {
        _fuel += 1;
        rBar.SetValue(_fuel);
        Debug.Log(_fuel);
        // enable the launch button when the refuel is finished
        if (_fuel >= maxFuel)
        {
            launch.interactable = true;
        }
    }

    public void LoadSetFuel(int f)
    {
        // load refuel state when loading a game
        _fuel = f;
        if (rBar.gameObject.activeSelf)
        {
            rBar.SetValue(_fuel);
        }
    }
}
