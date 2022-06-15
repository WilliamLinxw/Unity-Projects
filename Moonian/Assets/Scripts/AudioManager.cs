using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    // This deals with plying sound effect and background music
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        // only allow one instance to exist
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.spatialBlend = s.spatialBlend;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Background_1");
    }

    public void Play(string name)
    {
        // play by entering a string name, which is stored as a list
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }
}
