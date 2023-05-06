using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string soundName;

    public AudioClip soundClip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;

    public AudioMixerGroup musicMixerGroup;

    [HideInInspector] public AudioSource audioSource;
}
