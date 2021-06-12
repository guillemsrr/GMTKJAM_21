using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup _audioMixerGroup;
    [SerializeField]
    private AudioSource[] _audioSources = new AudioSource[10];

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;

        for(int x = 0; x < _audioSources.Length; x++)
        {
            _audioSources[x] = gameObject.AddComponent<AudioSource>() as AudioSource;
            _audioSources[x].outputAudioMixerGroup = _audioMixerGroup;
        }
    }

    public void Play(AudioClip clip)
    {
        foreach (var audio in _audioSources)
        {
            if (!audio.isPlaying)
            {
                audio.clip = clip;
                audio.Play();
            }          
        }
    }
}
