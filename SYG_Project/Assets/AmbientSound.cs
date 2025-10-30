using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    AudioSource Source=>GetComponent<AudioSource>();
    // Start is called before the first frame update
    void OnEnable()
    {
        Source.loop = true;
        EVENTS.OnGameStart += Play;
        EVENTS.OnGameOver += Pause;
    }

    // Update is called once per frame
    void OnDisable()
    {
        EVENTS.OnGameStart -= Play;
        EVENTS.OnGameOver -= Pause;
    }

    void Play()
    {
        Source.Play();
    }

    void Pause()
    {
        Source.Pause();
    }
}
