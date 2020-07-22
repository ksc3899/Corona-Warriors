﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            //s.source.pitch = s.pitch;
        }
        PlayBackGroundMusic();
    }

    public void PlayBackGroundMusic()
    {
        sounds[0].source.Play();
        sounds[0].source.loop = true;
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void StopSound()
    {
        sounds[0].source.Stop();
    }
}