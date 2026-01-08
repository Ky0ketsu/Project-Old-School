using System.Collections;
using System.Collections.Generic;
using System.Data;
using Rewired;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_Tuto : MonoBehaviour
{
    public GameObject grandpaTuto;
    public GameObject door;
    public Scr_TabletMove scriptTablet;
    public GameObject tablet; 

    public bool tutoCompleted = false;

    public bool didHeSlap = false;
    public bool didHeCamera = false;
    bool tabletStopped = false;
    
    public float timer = 0;

    private Player player;


    private void Start()
    {
        player = ReInput.players.GetPlayer(0);
    }
    private void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) { return; }
        if (tutoCompleted == true)
        {
            Destroy(this); 
        }
        if (tutoCompleted == false)
        {
            gameObject.GetComponent<PlayerMove>().enabled = false;
            gameObject.GetComponent<PlayerLook>().enabled = false;

        }

        if (didHeSlap == false)
        {
            if (tabletStopped == false)
            {
                scriptTablet = Object.FindAnyObjectByType<Scr_TabletMove>();
                GameObject TabletGO = scriptTablet.gameObject;
                TabletGO.GetComponent<Scr_TabletMove>().enabled = false;
                if (TabletGO.GetComponent<Scr_TabletMove>().enabled == false)
                {
                    tabletStopped = true;
                }
            }
        }

        if (didHeSlap == true)
        {
            GameObject TabletGO = scriptTablet.gameObject;
            TabletGO.GetComponent<Scr_TabletMove>().enabled = true;

            if (player.GetButton("Slap"))
            {
                if (timer >= 0.25f)
                {
                    didHeCamera = true;
                    timer = 0;
                }
                timer += Time.deltaTime;
            }
            if (player.GetButtonUp("Slap"))
            {
                timer = 0; 
            }
        }
        
        if (didHeCamera == true)
        {
            didHeSlap = false;
            gameObject.GetComponent<PlayerMove>().enabled = true;
            gameObject.GetComponent<PlayerLook>().enabled = true;
            tutoCompleted = true; 
        }
        
    }

    




}
