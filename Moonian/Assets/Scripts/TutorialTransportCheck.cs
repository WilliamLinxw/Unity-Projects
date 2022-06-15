using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// check if the player has passed the first portal to the base
public class TutorialTransportCheck : MonoBehaviour
{
    public Button helpBoxOK;
    public GameObject portal1;
    private Vector3 portal1Pos;
    void Start()
    {
        portal1Pos = portal1.transform.position;
    }

    void Update()
    {
        if (Player.Instance != null)
        {
            Vector3 pos = Player.Instance.transform.position;
            Vector3 diff = pos - portal1Pos;
            if (Vector3.Magnitude(diff) <= 5)
            {
                helpBoxOK.interactable = true;
                Destroy(this);
            }
        }
    }
}
