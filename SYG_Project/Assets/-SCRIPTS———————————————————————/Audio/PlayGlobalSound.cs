using UnityEngine;
public class PlayGlobalSound : MonoBehaviour
{
    [SerializeField] AudioClipExtended sound;
    [SerializeField] bool playOnEnable = true;
    [SerializeField] bool oneShot = false;

    void OnEnable()
    {
        if (playOnEnable) Play(); 
    }

    public void Play()
    {
        if (sound.clip) MENU.SCRIPT.Audio.PlayOneShot(sound.clip, sound.volume);
        if (oneShot) Destroy(this);
    }

} // SCRIPT END
