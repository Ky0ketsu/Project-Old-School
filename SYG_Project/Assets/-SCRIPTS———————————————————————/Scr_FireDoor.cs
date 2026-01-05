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

    [SerializeField]
    Transform rightDoor;
    public float rightRotaClose;
    public float rightRotaOpen;

    [SerializeField]
    Transform leftDoor;
    public float leftRotaClose;
    public float leftRotaOpen;

    public void Slap()
    {
        ChangeDoorState();
    }

    public void ChangeDoorState()
    {
        isClosed = !isClosed;

        if (isClosed == true)
        {
            CloseDoor();
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
        isClosed = true;
        ChangeDoorState();
        initialY = transform.position.y;
    }

    public void CloseDoor()
    {
        Debug.Log("Porte fermer");
        rightDoor.DORotate(Vector3.up * rightRotaClose, 1f).SetEase(Ease.InCubic);
        leftDoor.DORotate(Vector3.up * leftRotaClose, 1f).SetEase(Ease.InCubic);

        int r = Random.Range(0, closeList.Count);
        audioFireDoor.clip = closeList[r];
        audioFireDoor.Play();
    }

    public void OpenDoor()
    {
        rightDoor.DORotate(Vector3.up * rightRotaOpen, 1f).SetEase(Ease.InCubic);
        leftDoor.DORotate(Vector3.up * leftRotaOpen, 1f).SetEase(Ease.InCubic);
        Debug.Log("Porte ouverte");

        int r = Random.Range(0, openList.Count);
        audioFireDoor.clip = openList[r];
        audioFireDoor.Play();
    }

}
