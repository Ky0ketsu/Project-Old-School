using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scr_ButtonFireDoor : MonoBehaviour, ISlapable
{
    public float timer = 15f;
    public bool isClosed = false;
    public GameObject doorToSpawn;
    public GameObject loadedDoor;

    [Range(0, 50f)] public float timerAutoOpenDoor;
    [SerializeField] private float currentTimer;

    public void Slap()
    {
        ChangeDoorState();

        timer = 15f;
    }

    void ChangeDoorState()
    {
        isClosed = !isClosed;

        if (isClosed == true)
        {
            CloseDoor();
            currentTimer = timerAutoOpenDoor;
        }
        else
        {
            OpenDoor();
        }
    }    

    void CloseDoor()
    {
        Debug.Log("Porte fermer");
    }

    void OpenDoor()
    {
        Debug.Log("Porte ouverte");
    }

    public void Update()
    {
        if (isClosed == true)
        {
           if (timer > 0) timer -= Time.deltaTime;
           if (timer <= 0) ChangeDoorState();
        }  
    }
}
