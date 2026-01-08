using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : MonoBehaviour , IAudioService
{
    void Awake()
    {
        ServicesLocator.Register<IAudioService>(this);
    }

    void OnDestroy()
    {
        ServicesLocator.Unregister<IAudioService>();
    }

    private AudioSource _audioData;

    void Start()
    {
        _audioData = GetComponent<AudioSource>();
    }

    public void PlayAudioOneShot(AudioClip audio)
    {
        _audioData.PlayOneShot(audio);
        Debug.Log("Je lance un son OneShot");
    }


    public void PlayAudio(AudioClip audio)
    {
        _audioData.clip = audio;
        _audioData.Play(0);
        Debug.Log("Je lance le son");
    }
    public void StopAudio()
    {
        _audioData.Stop();
    }

    public void PauseAudio()
    {
        _audioData.Pause();
    }

    public void ResumeAudio()
    {
        _audioData.UnPause();
    }

    
}
