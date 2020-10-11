using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        // Don't Destroy on load another scene, and deletes if there's another AudioManager
        if (instance == null)
            instance = this;
        else
        {
            DestroyObject(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        // Assign all data from Sound base class
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip_;

            s.source.volume = s.volume_;
            s.source.pitch = s.pitch_;
            s.source.loop = s.loop_;
        }
    }

    private void Start()
    {
        //Play("Music");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return false;
        }

        return s.source.isPlaying;
    }
}
