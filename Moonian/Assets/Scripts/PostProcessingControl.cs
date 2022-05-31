using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingControl : MonoBehaviour
{
    // public GameObject globalLayer;
    // public GameObject baseLayer;
    // private PostProcessVolume globalVolume;
    // private PostProcessVolume baseVolume;
    public GameObject dyingLayer;
    private PostProcessVolume dyingVolume;
    private float healthThreshold = 40;
    
    void Start()
    {
        // globalVolume = globalLayer.GetComponent<PostProcessVolume>();
        // baseVolume = baseLayer.GetComponent<PostProcessVolume>();
        dyingVolume = dyingLayer.GetComponent<PostProcessVolume>();

        dyingVolume.weight = 0;
    }

    void Update()
    {
        float health = PlayerProperty.Instance.currentHealth;
        float dweight = 0;  // weight for the dying layer; this mainly changes the saturation
        if (health <= healthThreshold)
        {
            dweight = (healthThreshold - health) / healthThreshold;
        }
        dweight = Mathf.Clamp(dweight, 0, 1);
        dyingVolume.weight = dweight;
        if (health <= healthThreshold / 2)
        {
            if (FindObjectOfType<AudioManager>().sounds[2].source.isPlaying) return;  // check if it's already playing
            FindObjectOfType<AudioManager>().Play("Heartbeat");
        }
        else
        {
            FindObjectOfType<AudioManager>().Stop("Heartbeat");
        }
    }
}
