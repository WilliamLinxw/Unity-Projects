using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidingDoor_rec : MonoBehaviour
{
    public Vector3 endPos_rec;
    public float speed_rec = 1.0f;

    private bool moving_rec = false;
    private bool opening_rec = true;
    private Vector3 startPos_rec;
    private float delay_rec = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos_rec = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving_rec)
        {
            if (opening_rec)
            {
                MoveDoor(endPos_rec);
            }
            else
            {
                MoveDoor(startPos_rec);
            }
        }
    }

    void MoveDoor(Vector3 goalPos)
    {
        float dist = Vector3.Distance(transform.localPosition, goalPos);
        if (dist > .01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, goalPos, speed_rec * Time.deltaTime);
        }
        else
        {
            if (opening_rec)
            {
                delay_rec += Time.deltaTime;
                if (delay_rec > 2.0f)
                {
                    opening_rec = false;
                }
            }
            else
            {
                moving_rec = false;
                opening_rec = true;
            }
        }
    }

    public bool Moving_rec
    {
        get { return moving_rec; }
        set { moving_rec = value; }
    }
}
