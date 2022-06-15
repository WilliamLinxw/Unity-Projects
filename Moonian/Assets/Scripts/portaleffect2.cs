using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calculate the relative position between the player camera and door1, move door2's camera to the position
// relative to door2 as player camera relative to door1
// Detailed comments in portaleffect
public class portaleffect2: MonoBehaviour
{
    public Transform door1;
    public Transform door2;
    public Transform playerCamera;
    private Vector3 relativeVector;
    public Camera portalCamera;

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

        var rotationVector = playerCamera.transform.rotation.eulerAngles;
        rotationVector.x -= 10f;
        this.transform.rotation = Quaternion.Euler(rotationVector);
    }
}
