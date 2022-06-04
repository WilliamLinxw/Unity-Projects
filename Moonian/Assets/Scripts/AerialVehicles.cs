using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialVehicles : MonoBehaviour
{
    Vector3 dest, diff;
    public int step = 1000;
    int counter = 0;
    void Start()
    {
        dest = RandomDest();
        gameObject.transform.position = RandomDest();
        diff = dest - gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += diff / step;
        counter ++;
        if (counter >= step * 0.99)
        {
            dest = RandomDest();
            diff = dest - gameObject.transform.position;
            counter = 0;
            
        }
    }

    private Vector3 RandomDest()
    {
        float x = Random.Range(-4000, 4000);
        float y = Random.Range(900, 1300);
        float z = Random.Range(-4000, 4000);
        return new Vector3(x, y, z);
    }
}
