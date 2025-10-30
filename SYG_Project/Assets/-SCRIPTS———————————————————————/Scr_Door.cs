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

    public void Slap()
    {
        slaped = true;
    }


    //Quand Scr_Character dit être derrière une porte, il lance un timer, si le timer est en dessous de 5 le visuel change
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


    //La porte est fermé (par le Vieux ou le Joueur) et reviens à un visuel de base + arrête les changements dans l'Update
    public void CloseDoor()
    {
        transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        turnVisual = false;
        halfOpen = false;
        if (closeDoorFx != null) Instantiate(closeDoorFx, transform.position, Quaternion.identity);
        else Debug.Log("Pas de FX");
    }
}
