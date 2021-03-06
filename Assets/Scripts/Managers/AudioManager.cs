using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixerGroupMaster;
    [SerializeField] private AudioMixerGroup _audioMixerGroupSFX;
    [SerializeField] private AudioSource[] _audioSources = new AudioSource[10];
    

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;

        for(int x = 0; x < _audioSources.Length; x++)
        {
            _audioSources[x] = gameObject.AddComponent<AudioSource>() as AudioSource;
            _audioSources[x].outputAudioMixerGroup = _audioMixerGroupSFX;
        }
    }

    public void Play(AudioClip clip)
    {
        if (clip == null) return;
        foreach (var audio in _audioSources)
        {
            if (!audio.isPlaying)
            {
                audio.clip = clip;
                audio.Play();
                return;
            }          
        }
    }

    public void Mute()
    {
        _audioMixerGroupMaster.SetFloat("Volume", -80f);
    }

    public void Unmute()
    {
        _audioMixerGroupMaster.SetFloat("Volume", 0f);
    }
}
