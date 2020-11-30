﻿using UnityEngine.Audio;
using System;
using UnityEngine;

/*
    Sources:
    1) B., Brackeys, 'Introduction to AUDIO in Unity', 2017[Online] Available: https://www.youtube.com/watch?v=6OT43pvUyfY [Accessed: Nov- 28-2020].
*/

public class AudioManager : MonoBehaviour {
    
    public Sound[] _sounds;
    
    void Awake() {
        foreach(Sound _s in _sounds) {
            _s._source = gameObject.AddComponent<AudioSource>();

            _s._source.clip = _s._audioClip;
            _s._source.volume = _s._volume;
            _s._source.pitch = _s._pitch;
        }    
    }

    public void Play(string _name) {
        Sound _s = Array.Find(_sounds, _sound => _sound._name == _name);
        _s._source.Play();
    }
}