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

    public List<AudioClip> openList;
    public List<AudioClip> closeList;
    [SerializeField] AudioSource audioFireDoor;

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
        int r = Random.Range(0, closeList.Count);
        audioFireDoor.clip = closeList[r];
        audioFireDoor.Play();
    }

    void OpenDoor()
    {
        transform.DORotate(Vector3.up * 90f, 1f).SetEase(Ease.InCubic);
        Debug.Log("Porte ouverte");
        int r = Random.Range(0, openList.Count);
        audioFireDoor.clip = openList[r];
        audioFireDoor.Play();
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
