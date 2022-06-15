using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The script that generates the video of the crashing

public class crashMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 20);
        transform.Rotate(-0.05f, 0, 0.01f);
    }
}
