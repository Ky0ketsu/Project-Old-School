using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scr_DroneAmbiance : MonoBehaviour
{
    public List<AudioClip> droneList;
    public AudioSource drone;
    public Scr_GameTimer timer;

    


    // Start is called before the first frame update
    void Start()
    {

        EVENTS.OnGameplay += PlaySound;
        timer = ServicesLocator.Get<TimerService>().timerObject.GetComponent<Scr_GameTimer>();

    
        

    }

    void Update()
    {

        if (GAME.MANAGER.CurrentState != State.gameplay)
        {
            return;
        }
        

        if(!drone.isPlaying && timer.currentTimer > 0)
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        if(!drone.isPlaying)
        {

         int r = Random.Range(0, droneList.Count);
            AudioClip clip = droneList[r];
            drone.clip = clip;
            drone.Play();
            StartCoroutine(AmbPlay());
        }
    }


    
    IEnumerator AmbPlay()
    {
        yield return new WaitForSeconds(30);
        drone.Stop();
        PlaySound();
    }
}
    
