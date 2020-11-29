using UnityEngine.Audio;
using UnityEngine;

/*
    Sources:
    1) B., Brackeys, 'Introduction to AUDIO in Unity', 2017[Online] Available: https://www.youtube.com/watch?v=6OT43pvUyfY [Accessed: Nov- 28-2020].
*/

[System.Serializable]
public class Sound {

    public string _name;

    public AudioClip _audioClip;

    [Range(0f, 1f)]
    public float _volume;

    [Range(.1f, 3f)]
    public float _pitch;

    [HideInInspector]
    public AudioSource _source;
}
