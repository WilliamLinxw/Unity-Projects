using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// change the angle of the player's spot light
public class SpotAngle : MonoBehaviour
{
    public float deltaAngleRatio = 0.5f;
    Light pl;

    void Start()
    {
        pl = GetComponent<Light>();
    }

    void Update()
    {
        float deltaAngle = Input.GetAxis("Spot Angle Change");
        float angle = pl.spotAngle;
        angle += deltaAngle * deltaAngleRatio;
        pl.spotAngle = Mathf.Clamp(angle, 20, 120);  // limited to a range
    }
}
