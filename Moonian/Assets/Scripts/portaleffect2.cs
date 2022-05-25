using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaleffect2: MonoBehaviour
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


        Vector3 pos = this.transform.position;
        pos.x = relativeVector.z;
        pos.y = relativeVector.y;
        pos.z = relativeVector.x;
        this.transform.position = door2.position + pos;
    }
}
