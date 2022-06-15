using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchMovement : MonoBehaviour
{
    private float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(launch());

    }
    IEnumerator launch()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        speed += 0.01f;
        transform.Translate(0, speed, 0);
    }

}
