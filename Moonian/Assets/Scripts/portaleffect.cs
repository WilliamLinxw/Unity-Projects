using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaleffect : MonoBehaviour
{
    public Transform door1;
    public Transform door2;
    public Transform playerCamera;
    private Vector3 relativeVector;
    public Camera portalCamera;
    private bool flipped = false;

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

        //if (portalCamera.transform.position.z - door2.transform.position.z >= 0 && flipped == false)
        //{
        //    Debug.Log(flipped);
        //    Matrix4x4 mat = portalCamera.projectionMatrix;
        //    mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        //    portalCamera.projectionMatrix = mat;
        //    flipped = true;
        //}
        //if (portalCamera.transform.position.z - door2.transform.position.z < 0 && flipped == true)
        //{
        //    Debug.Log(flipped);
        //    Matrix4x4 mat = portalCamera.projectionMatrix;
        //    mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        //    portalCamera.projectionMatrix = mat;
        //    flipped = false;
        //}
    }

}
