using System.Collections;
using System.Collections.Generic;
using System.Data;
using Rewired;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_Tuto : MonoBehaviour
{
    public TabletAction scriptTablet;

    public bool tutoCompleted = false;

    public bool didHeSlap = false;
    public bool didHeCamera = false;
    bool tabletStopped = false;
    
    public float timer = 0;

    private Player player;

    public GameObject TutoText;
    TextMeshProUGUI textmeshpro;

    [SerializeField] PlayerActionService playerActionService;
    [SerializeField] Scr_GameTimer Scr_GameTimer;

    private void Start()
    {
        player = ReInput.players.GetPlayer(0);
        textmeshpro = TutoText.GetComponent<TextMeshProUGUI>();
        Scr_GameTimer = FindAnyObjectByType<Scr_GameTimer>();
    }
    private void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) { return; }
        if (tutoCompleted == true)
        {
            Scr_GameTimer.EnableTimer();
            Destroy(TutoText);
            Destroy(this); 
        }
        if (tutoCompleted == false)
        {
            gameObject.GetComponent<PlayerMove>().enabled = false;
            gameObject.GetComponent<PlayerLook>().enabled = false;
            textmeshpro.enabled = true; 

        }

        if (didHeSlap == false)
        {
            if (tabletStopped == false)
            {
                scriptTablet = Object.FindAnyObjectByType<TabletAction>();
                GameObject TabletGO = scriptTablet.gameObject;
                TabletGO.GetComponent<TabletAction>().enabled = false;

                playerActionService.TUTOCanUseTablet = false;

                Scr_GameTimer.DisableTimer();

                if (TabletGO.GetComponent<TabletAction>().enabled == false)
                {
                    tabletStopped = true;
                }
            }
        }

        if (didHeSlap == true)
        {
            textmeshpro.SetText("Hold Space or Left Click to look at your TABLET"); 
            GameObject TabletGO = scriptTablet.gameObject;
            TabletGO.GetComponent<TabletAction>().enabled = true;

            playerActionService.TUTOCanUseTablet = true;

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