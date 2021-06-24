using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour
{

    public float _soundTrackVolume = 1;
    [SerializeField]
    private float _initialVolume = 1;
    [SerializeField]
    private float _volumeRampSpeed = 4;
    [SerializeField]
    private bool _playOnStart = true;
    [SerializeField]
    private AudioSource[] _audioSources;

    private AudioSource _activeAudio, _fadeAudio;
    private float _volumeVelocity, _fadeVelocity;
    private float _volume;
    private Stack<string> _trackStack = new Stack<string>();

    public void PopTrack()
    {
        if (_trackStack.Count > 1)
            _trackStack.Pop();
        Enqueue(_trackStack.Peek());
    }

    public void Enqueue(string name)
    {
        foreach (var i in _audioSources)
        {
            if (i.name == name)
            {
                _fadeAudio = _activeAudio;
                _activeAudio = i;
                if (!_activeAudio.isPlaying) _activeAudio.Play();
                break;
            }
        }
    }

    public void Play()
    {
        if (_activeAudio != null)
            _activeAudio.Play();
    }

    public void Stop()
    {
        foreach (var i in _audioSources) i.Stop();
    }

    void OnEnable()
    {
        _trackStack.Clear();
        if (_audioSources.Length > 0)
        {
            _activeAudio = _audioSources[0];
            foreach (var i in _audioSources) i.volume = 0;
            _trackStack.Push(_audioSources[0].name);
            if (_playOnStart) Play();
        }
        _volume = _initialVolume;
    }

    void Reset()
    {
        _audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        _volume = volume;
    }

    void Update()
    {
        if (_activeAudio != null)
            _activeAudio.volume = Mathf.SmoothDamp(_activeAudio.volume, _volume * _soundTrackVolume, ref _volumeVelocity, _volumeRampSpeed, 1);

        if (_fadeAudio != null)
        {
            _fadeAudio.volume = Mathf.SmoothDamp(_fadeAudio.volume, 0, ref _fadeVelocity, _volumeRampSpeed, 1);
            if (Mathf.Approximately(_fadeAudio.volume, 0))
            {
                _fadeAudio.Stop();
                _fadeAudio = null;
            }
        }
    }
}
