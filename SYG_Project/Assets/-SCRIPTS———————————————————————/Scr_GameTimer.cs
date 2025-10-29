using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_GameTimer : MonoBehaviour
{
    public float timer; //60 min = 300 sec

    [HideInInspector]
    private bool isPlaying;

    private void Awake()
    {
        EVENTS.OnGameplay += EnableTimer;
        EVENTS.OnGameplayExit += DisableTimer;
        EVENTS.OnGameStart += SetTimer;
    }

    private void OnDestroy()
    {
        EVENTS.OnGameplay -= EnableTimer;
        EVENTS.OnGameplayExit -= DisableTimer;
        EVENTS.OnGameStart -= SetTimer;
    }

    private void EnableTimer()
    {
        isPlaying = true;
    }

    public void DisableTimer()
    {
        isPlaying = false;
    }

    void SetTimer()
    {
        timer = 10f;
    }

    public void Update()
    {
        if(isPlaying)
        {
            TimerUpdate();
        }
    }

    void TimerUpdate()
    {
        if (timer > 0) timer -= Time.deltaTime;

        if (timer <= 0) EVENTS.InvokeGameOver();
    }
}
