using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaleffect : MonoBehaviour
{
    public Transform door1;
    public Transform door2;
    public Transform playerCamera;
    public Vector3 relativeVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        relativeVector = playerCamera.position - door1.position;
        this.transform.position = door2.position - relativeVector;
    }
}
