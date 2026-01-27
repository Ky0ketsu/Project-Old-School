using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_GameTimer : MonoBehaviour
{
    [SerializeField,Range(10, 300)]
    float initialTimer;
    private bool alreadyPlayed = false;
    public List<AudioClip> timerList;
    [SerializeField] AudioSource audioTimer;

    [HideInInspector]
    public float currentTimer;

    public Scr_DroneAmbiance droneScript;



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
        droneScript.gameObject.SetActive(true);
    }

    public void DisableTimer()
    {
        isPlaying = false;
    }

    public void SetTimer()
    {
        currentTimer = initialTimer;
    }

    public void Update()
    {
        if(isPlaying)
        {
            TimerUpdate();
            if (currentTimer <= 60 && alreadyPlayed == false)
            {
                int r = Random.Range(0, timerList.Count);
                audioTimer.clip = timerList[r];
                audioTimer.Play();

                alreadyPlayed = true;
            }
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

            droneScript.gameObject.SetActive(false);
        }
    }
}
