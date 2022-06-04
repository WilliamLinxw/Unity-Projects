using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPickupCheck : MonoBehaviour
{
    public List<GameObject> tutorialPickups;
    public GameObject spotLight;
    public Button okButton;
    private int inactiveNum = 0;
    void Start()
    {
        foreach (GameObject g in tutorialPickups)
        {
            g.SetActive(true);
        }
        spotLight.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject g in tutorialPickups)
        {
            if (!g.activeSelf)
            {
                inactiveNum += 1;
                spotLight.SetActive(false);
            }
        }
        if (inactiveNum == tutorialPickups.Count)
        {
            okButton.interactable = true;
        }
    }
}
