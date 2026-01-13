using UnityEngine;

public interface IAudioService
{
    public void PlayAudioOneShot(AudioClip audio);

    public void PlayAudio(AudioClip audio);
    public void StopAudio();
    public void PauseAudio();
    public void ResumeAudio();
}
