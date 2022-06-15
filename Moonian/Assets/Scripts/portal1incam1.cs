using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Check if the portal is in the view of the portal camera, if so, make the portal invisible
public class portal1incam1 : MonoBehaviour
{
    public GameObject target;
    public Camera cam;

    private bool isVisible(Camera cam, GameObject target)
    {
        // Calculate the view frustum of the camera
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);

        // Check the distance between camera and the portal
        float distance = Vector3.Distance(cam.transform.position, target.transform.position);

        // center
        Vector3 position_center = target.transform.position;
        var point_center = position_center;


        // upper left
        Vector3 position_upperleft = target.transform.position;
        position_upperleft.x -= 5;
        position_upperleft.y += 5;
        var point_upperleft = position_upperleft;
 

        // upper right
        Vector3 position_upperright = target.transform.position;
        position_upperright.x += 5;
        position_upperright.y += 5;
        var point_upperright = position_upperright;


        // lower left
        Vector3 position_lowerleft = target.transform.position;
        position_lowerleft.x -= 5;
        position_lowerleft.y -= 5;
        var point_lowerleft = position_lowerleft;


        // lower left
        Vector3 position_lowerright = target.transform.position;
        position_lowerright.x += 5;
        position_lowerright.y -= 5;
        var point_lowerright = position_lowerright;


        // Only activate when the camera is near the portal
        if (distance <= 150)
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
        // Make the portal invisible when sighted by the camera
        if (isVisible(cam, target))
        {
            target.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            target.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
