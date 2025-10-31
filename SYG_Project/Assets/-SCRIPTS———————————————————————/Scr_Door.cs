using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Door : MonoBehaviour, ISlapable
{
    public bool slaped;
    public bool halfOpen = false;
    public bool turnVisual = false;

    [SerializeField]
    private GameObject closeDoorFx, openDoorFX;


    public List<AudioClip> dooropenList;
    public List<AudioClip> doorcloseList;
    [SerializeField] AudioSource audioDoor;

    public void Slap()
    {
        slaped = true;
    }


    //Quand Scr_Character dit �tre derri�re une porte, il lance un timer, si le timer est en dessous de 5 le visuel change
    //
    public void Update()
    {
        if (halfOpen == true)
        {
            if (turnVisual == false)
            {
                transform.rotation = new Quaternion(transform.rotation.x, -12f, transform.rotation.z, transform.rotation.w);
                turnVisual = true;
            }
        }
        
    }


    //La porte est ferm� (par le Vieux ou le Joueur) et reviens � un visuel de base + arr�te les changements dans l'Update
    public void CloseDoor()
    {
        transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        turnVisual = false;
        halfOpen = false;
        int r = Random.Range(0, doorcloseList.Count);
        audioDoor.clip = doorcloseList[r];
        audioDoor.Play();
        if (closeDoorFx != null) Instantiate(closeDoorFx, transform.position, Quaternion.identity);
        else Debug.Log("Pas de FX");
    }
}
