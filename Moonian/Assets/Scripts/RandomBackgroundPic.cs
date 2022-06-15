using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// generate a random background picture in the welcome scene
public class RandomBackgroundPic : MonoBehaviour
{
    public List<Sprite> bgs;

    void Start()
    {
        gameObject.GetComponent<Image>().sprite = bgs[Random.Range(0, 3)];
    }
}
