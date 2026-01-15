using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_GameTimer : MonoBehaviour
{
    [SerializeField,Range(10, 300)]
    float initialTimer;

    [HideInInspector]
    public float currentTimer;



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

    public void EnableTimer()
    {
        isPlaying = true;
    }

    public void DisableTimer()
    {
        isPlaying = false;
    }

    void SetTimer()
    {
        currentTimer = initialTimer;
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
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }

        if (currentTimer <= 0)
        {
            isPlaying=false;
            EVENTS.InvokeGameOver();
        }
    }
}
