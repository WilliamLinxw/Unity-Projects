using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaleffect : MonoBehaviour
{
    public Transform door1;
    public Transform door2;
    public Transform playerCamera;
    private Vector3 relativeVector;
    public Transform detection;
    public Transform target;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
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
        this.transform.rotation = Quaternion.Euler(rotationVector);

        //Vector3 viewPos = cam.WorldToViewportPoint(target.position);
        //Debug.Log(viewPos);
        //if((viewPos.x >= 0f && viewPos.x <= 1f) && (viewPos.y >= 0f && viewPos.y <= 1f))
        //{
        //    target.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        //    Debug.Log("portal detected");
        //}
        //else
        //{
        //    target.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        //    Debug.Log("not detected");
        //}
        //checkPortal();

    }

    void checkPortal()
    {
        RaycastHit hit;
 
        if (Physics.SphereCast(this.transform.position, 20f, transform.forward, out hit, 20f))
        {
            Debug.Log("check");
            if (hit.collider.gameObject.tag == "portal 2")
            {
                Debug.Log("portal 2 detected");
                detection = hit.transform;
                detection.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }

        }
    }
}
