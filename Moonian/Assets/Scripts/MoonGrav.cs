using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGrav : MonoBehaviour
{
    public float gravityY;

    void Start(){
        Physics.gravity = new Vector3(0, gravityY, 0);
    }
}
