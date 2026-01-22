using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DroneAmbiance : MonoBehaviour
{
    public List<AudioClip> droneList;
    public AudioSource drone;

    // Start is called before the first frame update
    void Start()
    {

        EVENTS.OnGameplay += PlaySound;
        
        

    }

    void PlaySound()
    {
        int r = Random.Range(0, droneList.Count);
        AudioClip clip = droneList[r];
        drone.clip = clip;
        drone.Play();
    }


    void OnDestroy()
    {
        EVENTS.OnGameplay -= PlaySound;
    }
}
