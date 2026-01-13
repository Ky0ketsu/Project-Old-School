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
        if (audio == null)
        {
            Debug.LogWarning("Il n'y a pas de son a joué");
            return; 
        }
        _audioData.PlayOneShot(audio);
        Debug.Log("Je lance un son OneShot");
    }


    public void PlayAudio(AudioClip audio)
    {
        if (_audioData == null)
        {
            Debug.LogWarning("il n'y a pas de son a joué");
            return;
        }

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
