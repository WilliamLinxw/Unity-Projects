using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeRocket : MonoBehaviour
{
    // public static EscapeRocket Instance;
    public int fuel {get { return _fuel;}}
    public RefuelBar rBar;
    public Button launch;
    private int _fuel;
    private int maxFuel = 30;

    void Start()
    {
        // Instance = this;
        _fuel = 0;
        launch.interactable = false;
    }
    void Update()
    {
        if (Player.Instance != null)
        {
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
        if (_fuel >= maxFuel)
        {
            launch.interactable = true;
        }
    }

    public void LoadSetFuel(int f)
    {
        _fuel = f;
        if (rBar.gameObject.activeSelf)
        {
            rBar.SetValue(_fuel);
        }
    }
}
