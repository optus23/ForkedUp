using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip_;
    [Range(0f, 1f)]
    public float volume_;
    [Range(0.1f, 3f)]
    public float pitch_;

    public bool loop_;

    [HideInInspector]
    public AudioSource source;
}
