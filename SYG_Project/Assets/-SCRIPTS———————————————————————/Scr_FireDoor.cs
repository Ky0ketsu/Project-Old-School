using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scr_FireDoor : MonoBehaviour, ISlapable
{
    public bool isClosed = false;
    public GameObject doorToSpawn;
    public GameObject loadedDoor;

    [Range(0, 50f)] public float timerAutoOpenDoor;
    [SerializeField] private float currentTimer;

    public void Slap()
    {
        ChangeDoorState();
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

    [HideInInspector]
    private float initialY;

    private void Start()
    {
        initialY = transform.position.y;
    }

    void CloseDoor()
    {
        Debug.Log("Porte fermer");
        transform.DORotate(Vector3.zero, 1f).SetEase(Ease.InCubic);
    }

    void OpenDoor()
    {
        transform.DORotate(Vector3.up * 90f, 1f).SetEase(Ease.InCubic);
        Debug.Log("Porte ouverte");
    }

    public void Update()
    {
        if (isClosed == false)
        {
           if (currentTimer > 0) currentTimer -= Time.deltaTime;
           if (currentTimer <= 0) ChangeDoorState();
        }  
    }
}
