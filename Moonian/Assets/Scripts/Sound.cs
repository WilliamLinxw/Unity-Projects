using UnityEngine.Audio;
using UnityEngine;

// for the music clips that can be played by the audio manager
[System.Serializable]
public class Sound {

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(.1f, 3f)]
    public float pitch = 1f;  // freq
    [Range(0f, 1f)]
    public float spatialBlend = 0f;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}