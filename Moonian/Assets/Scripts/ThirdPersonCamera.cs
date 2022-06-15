using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control of the third person camera
public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = -60.0f;
    private const float Y_ANGLE_MAX = 60.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 2.0f;
    public Transform Target;

    public Transform Obstruction;
    float zoomSpeed = 2f;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    private float sensitivityX = 20.0f;
    private float sensitivityY = 20.0f;
    private bool changed = false;
    public List<GameObject> obstructionList = new List<GameObject>();

    private void Start()
    {
        camTransform = transform;
        Obstruction = Target;
    }

    private void LateUpdate()
    {
        camControl();
        ViewObstructed();

    }

    // Move the camera according to the movement of the mouse
    void camControl()
    {
        if (!FindObjectOfType<GlobalControl>().gamePaused)
        {
            currentX += Input.GetAxis("Mouse X");
            currentY -= Input.GetAxis("Mouse Y");

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

            transform.LookAt(Target);

            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt.position + rotation * dir;
            camTransform.LookAt(lookAt.position);
        }
    }

    // If the camera is blocked by an object, make it invisible. Make it visible after the sight is clear
    void ViewObstructed()
    {
        RaycastHit hit;
        
        // Change the target position of the ray to a higher one for better performance
        if (!changed)
        {
            Vector3 position = Target.position;
            position.x = Target.position.x;
            position.y = Target.position.y+1.1f;
            position.z = Target.position.z;
            Target.position = position;
            changed = true;
        }
        

        Vector3 direction = Target.position - transform.position;

        // Shoot a ray from the camera to the user, check if it's block by non-player objects, if so, make them invisible
        // Restore them back after the sight is clear again
        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 13f))
        {
            Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
            if (hit.collider.gameObject.tag != "Base" && hit.collider.gameObject.tag != "Terrain")
            {
                if (hit.collider.gameObject.tag != "Player")
                {
                    // Construct a list to store all the objects that have blocked the sight
                    var isContain = obstructionList.Contains(hit.collider.gameObject);
                    if (!isContain)
                    {
                        obstructionList.Add(hit.collider.gameObject);
                    }
                    

                    // Make all the objects in the list invisible
                    foreach(GameObject obstruction in obstructionList)
                    {
                        if (obstruction.GetComponent<MeshRenderer>() != null)
                        {
                            obstruction.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                        }
                            
                    }
                    
                }

                // Restore objects back
                else
                {
                    foreach (GameObject obstruction in obstructionList)
                    {
                        if (obstruction.GetComponent<MeshRenderer>() != null)
                        {
                            obstruction.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                        }

                    }
                    obstructionList.Clear();
                }
            }
            
        }
    }
}
