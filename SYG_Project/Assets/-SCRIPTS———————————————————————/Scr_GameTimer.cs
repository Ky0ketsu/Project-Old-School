using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_GameTimer : MonoBehaviour
{
    [SerializeField] float timer;
    public float maxTime = 300; //5 min = 300 sec


    Text t => GetComponent<Text>();

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
        timer = maxTime;
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
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            t.text = timer.ToString();
        }

        if (timer <= 0)
        {
            isPlaying=false;
            EVENTS.InvokeGameOver();
        }
    }
}
