using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioFX : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [Space]
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _musicSource;
    [Space]
    [SerializeField] private AudioClip[] _buttonAudioClips;
    [SerializeField] private AudioClip[] _musicAudioClips;
    [Space]
    [SerializeField] private AudioClip[] _hitaudioClips;

    private AudioClip _playedClip;

    private void Start()
    {
        if(_musicAudioClips.Length == 0) return;
        _playedClip = _musicSource.clip = _musicAudioClips[Random.Range(0, _musicAudioClips.Length)];
        _musicSource.Play();
    }

    private void Update()
    {
        if (!_musicSource.isPlaying && _musicAudioClips.Length != 0)
        {
            AudioClip newClip = _musicAudioClips[Random.Range(0, _buttonAudioClips.Length)];
            
            while (newClip == _playedClip)
            {
                newClip = _musicAudioClips[Random.Range(0, _buttonAudioClips.Length)];
            }

            _playedClip = newClip;
            _musicSource.clip = newClip;
            _musicSource.Play();
        }
    }

    public void SetSound(float value)
    {
        _mixer.SetFloat("Sound", Mathf.Log10(value) * 20);
    }
    
    public void SetMusic(float value)
    {
        _mixer.SetFloat("Music", Mathf.Log10(value) * 20);
    }

    public void PlayHitSound()
    {
        if (_hitaudioClips.Length == 0) return;
        _soundSource.clip = _hitaudioClips[Random.Range(0, _hitaudioClips.Length)];
        _soundSource.Play();
    }

    public void PlayButtonSound()
    {
        if(_buttonAudioClips.Length == 0) return;
        _soundSource.clip = _buttonAudioClips[Random.Range(0, _buttonAudioClips.Length)];
        _soundSource.Play();
    }
}
