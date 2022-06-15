using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Open the door when it is triggered by the player
public class slidingDoor : MonoBehaviour
{
    public Vector3 endPos;
    public float speed = 1.0f;

    private bool moving = false;
    private bool opening = true;
    private Vector3 startPos;
    private float delay = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Only open the door when it's close, close the door after it opens.
        if (moving)
        {
            if (opening)
            {
                MoveDoor(endPos);
            }
            else
            {
                MoveDoor(startPos);
            }
        }
    }

    // Move the door to the goalPos
    void MoveDoor(Vector3 goalPos)
    {
        float dist = Vector3.Distance(transform.localPosition, goalPos);
        if(dist > .1f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, goalPos, speed * Time.deltaTime);
        }
        else
        {
            if (opening)
            {
                delay += Time.deltaTime;
                if (delay > 2.0f)
                {
                    opening = false;
                }
            }
            else
            {
                moving = false;
                opening = true;
            }
        }
    }

    public bool Moving
    {
        get { return moving;  }
        set { moving = value; }
    }
}
