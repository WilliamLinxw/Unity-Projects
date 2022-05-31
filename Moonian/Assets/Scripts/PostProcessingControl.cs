using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingControl : MonoBehaviour
{
    public GameObject dyingLayer;
    private PostProcessVolume dyingVolume;
    private float healthThreshold = 40;

    public GameObject MenuLayer;
    
    void Start()
    {
        dyingVolume = dyingLayer.GetComponent<PostProcessVolume>();
        dyingVolume.weight = 0;

        // menu layer is default set to inactive
        MenuLayer.SetActive(false);
        // but the default weight is set to 0 during game design; set to 1 at runtime
        MenuLayer.GetComponent<PostProcessVolume>().weight = 1;
    }

    void Update()
    {
        if (!FindObjectOfType<GlobalControl>().gamePaused)
        {
            MenuLayer.SetActive(false);
            AdjustDyingVolumeWeight();
        }
        else
        {
            MenuLayer.SetActive(true);
        }
    }

    private void AdjustDyingVolumeWeight()
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
