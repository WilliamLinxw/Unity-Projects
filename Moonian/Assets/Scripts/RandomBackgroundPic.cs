using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackgroundPic : MonoBehaviour
{
    public List<Sprite> bgs;

    void Start()
    {
        gameObject.GetComponent<Image>().sprite = bgs[Random.Range(0, 3)];
    }
}
