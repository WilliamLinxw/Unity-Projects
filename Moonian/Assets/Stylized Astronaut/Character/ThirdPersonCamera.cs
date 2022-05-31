using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void ViewObstructed()
    {
        RaycastHit hit;
        //Debug.Log("target:" + Target.position);
        //Debug.Log("transform:" + transform.position);

        
        if (!changed)
        {
            Vector3 position = Target.position;
            position.x = Target.position.x;
            position.y = Target.position.y+1.1f;
            position.z = Target.position.z;
            Target.position = position;
            changed = true;
        }
        

        //Debug.Log("target after:" + Target.position);
        Vector3 direction = Target.position - transform.position;
        //Debug.Log("direction:" + direction);
        //Debug.Log("forward:" + transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 13f))
        {
            Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
            if (hit.collider.gameObject.tag != "Base" && hit.collider.gameObject.tag != "Terrain")
            {
                if (hit.collider.gameObject.tag != "Player")
                {
                    //Debug.Log("not player");
                    //Debug.Log(hit.collider.gameObject.tag);
                    var isContain = obstructionList.Contains(hit.collider.gameObject);
                    if (!isContain)
                    {
                        obstructionList.Add(hit.collider.gameObject);
                    }
                    
                    Debug.Log(obstructionList.ToArray().Length);


                    foreach(GameObject obstruction in obstructionList)
                    {
                        if (obstruction.GetComponent<MeshRenderer>() != null)
                        {
                            obstruction.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                        }
                            
                    }
                    
                    //Obstruction = hit.transform;
                    //if (Obstruction.gameObject.GetComponent<MeshRenderer>() != null)
                    //{
                    //   Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    //}
                    
                    //Debug.Log("distance obstruction to transform:" + Vector3.Distance(Obstruction.position, transform.position));
                    //Debug.Log("distance target to transform:" + Vector3.Distance(transform.position, Target.position));
                    //if (Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                    //{
                    //    Debug.Log("zoom");
                    //    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                    //}
                }
                else
                {
                    Debug.Log(obstructionList.ToArray().Length);
                    foreach (GameObject obstruction in obstructionList)
                    {
                        if (obstruction.GetComponent<MeshRenderer>() != null)
                        {
                            obstruction.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                        }

                    }
                    obstructionList.Clear();

                    //Debug.Log("player");
                    //if (Obstruction.gameObject.GetComponent<MeshRenderer>() != null)
                    //{
                    //    
                    //
                    //    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    //}

                    //if (Vector3.Distance(transform.position, Target.position) < 4.5f)
                    //{
                    //    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                    //}

                }
            }
            
        }
    }
}
