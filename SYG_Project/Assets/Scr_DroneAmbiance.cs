using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DroneAmbiance : MonoBehaviour
{
    public List<AudioClip> droneList;
    public AudioSource drone;
    private bool ambPlayed = false;

    // Start is called before the first frame update
    void Start()
    {

        EVENTS.OnGameplay += ResumeSound;
        EVENTS.OnGameplayExit += StopSound;

        
        

    }

    void PlaySound()
    {
        if(ambPlayed == false)
        {

         int r = Random.Range(0, droneList.Count);
            AudioClip clip = droneList[r];
            drone.clip = clip;
            drone.Play();
            ambPlayed = true;
            StartCoroutine(AmbPlay());
        }
    }

    void StopSound()
    {
        ambPlayed = true;
    }
    void ResumeSound()
    {
        ambPlayed = false;
        PlaySound();
    }


    void OnDestroy()
    {
        EVENTS.OnGameplay -= ResumeSound;
        EVENTS.OnGameplayExit -= StopSound;
    }

    IEnumerator AmbPlay()
    {
        yield return new WaitForSeconds(7f);
        PlaySound();
    }
}
    
