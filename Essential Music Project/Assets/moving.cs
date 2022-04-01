using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public Vector3 positionChange;
    int flag = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= -5)
        {
            flag = 1;
        }
        else if(transform.position.z >= 0)
        {
            flag = -1;
        }
        transform.position += (positionChange * flag * Time.deltaTime);

    }
}
