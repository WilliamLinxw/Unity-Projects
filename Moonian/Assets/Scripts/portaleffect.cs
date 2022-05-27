using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaleffect : MonoBehaviour
{
    public Transform door1;
    public Transform door2;
    public Transform playerCamera;
    private Vector3 relativeVector;
    private Vector3 relativeVectorRotate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        relativeVector = playerCamera.position - door1.position;


        Vector3 pos = this.transform.position;
        pos.x = relativeVector.x;
        pos.y = relativeVector.y;
        pos.z = relativeVector.z;
        this.transform.position = door2.position + pos;

    }
}
