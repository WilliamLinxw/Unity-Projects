using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal4incam4 : MonoBehaviour
{
    public GameObject target;
    public Camera cam;

    private bool isVisible(Camera cam, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        //Debug.Log("camera:" + cam.transform.position);
        //Debug.Log("portal:" + target.transform.position);
        float distance = Vector3.Distance(cam.transform.position, target.transform.position);
        //Debug.Log("distance:" + distance);

        // center
        Vector3 position_center = target.transform.position;
        var point_center = position_center;
        //Debug.Log("center:" + point_center);

        // upper left
        Vector3 position_upperleft = target.transform.position;
        position_upperleft.x -= 5;
        position_upperleft.y += 5;
        var point_upperleft = position_upperleft;
        //Debug.Log("upperleft:" + point_upperleft);

        // upper right
        Vector3 position_upperright = target.transform.position;
        position_upperright.x += 5;
        position_upperright.y += 5;
        var point_upperright = position_upperright;
        //Debug.Log("upperright:" + point_upperright);

        // lower left
        Vector3 position_lowerleft = target.transform.position;
        position_lowerleft.x -= 5;
        position_lowerleft.y -= 5;
        var point_lowerleft = position_lowerleft;
        //Debug.Log("lowerleft:" + point_lowerleft);

        // lower left
        Vector3 position_lowerright = target.transform.position;
        position_lowerright.x += 5;
        position_lowerright.y -= 5;
        var point_lowerright = position_lowerright;
        //Debug.Log("lowerright:" + point_lowerright);


        if (distance <= 200)
        {
            foreach (var plane in planes)
            {
                if (plane.GetDistanceToPoint(point_center) < 0f && plane.GetDistanceToPoint(point_upperleft) < 0f
                    && plane.GetDistanceToPoint(point_upperright) < 0f && plane.GetDistanceToPoint(point_lowerleft) < 0f
                    && plane.GetDistanceToPoint(point_lowerright) < 0f)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    private void Update()
    {
        //var targetRender = target.GetComponent<Renderer>();
        //Debug.Log("log");
        if (isVisible(cam, target))
        {
            target.GetComponent<MeshRenderer>().enabled = false;
            //Debug.Log("detected");
        }
        else
        {
            target.GetComponent<MeshRenderer>().enabled = true;
            //target.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
