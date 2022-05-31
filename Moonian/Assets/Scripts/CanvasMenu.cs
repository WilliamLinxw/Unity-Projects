using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    public GameObject Menu;
    void Start()
    {
        Menu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Esc Menu"))
        {
            Menu.SetActive(!Menu.activeSelf);
        }
    }
}
