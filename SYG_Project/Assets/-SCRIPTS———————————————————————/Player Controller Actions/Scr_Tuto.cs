using System.Collections;
using System.Collections.Generic;
using System.Data;
using Rewired;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_Tuto : MonoBehaviour
{
    public bool CAN_THE_PLAYER_MOVE_ON_TUTO = true; 

    
    public TabletAction scriptTablet;

    public bool tutoCompleted = false;

    public bool didHeSlap = false;
    public bool didHeCamera = false;
    bool tabletStopped = false;
    
    public float timerForCamera = 0;
    public float timerForTextTuto = 0; 

    private Player player;

    public GameObject TutoText;

    [SerializeField] PlayerActionService playerActionService;
    [SerializeField] Scr_GameTimer Scr_GameTimer;

    [SerializeField] GameObject DoorToOpen;
    [SerializeField] GameObject[] GRANDPA_TO_START;
    [SerializeField] GameObject GRANDPAS;

    private void Start()
    {
        player = ReInput.players.GetPlayer(0);

        Scr_GameTimer = FindAnyObjectByType<Scr_GameTimer>();
        DoorToOpen.GetComponent<Scr_FireDoor>().CAN_BE_CLOSED = false;

        for(int i = 0; i < GRANDPA_TO_START.Length; i++)
        {
            GRANDPA_TO_START[i] = GRANDPAS.transform.GetChild(i).gameObject;
            GRANDPA_TO_START[i].gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) { return; }
        if (tutoCompleted == true)
        {
            for (int i = 0; i < GRANDPA_TO_START.Length; i++)
            {
                GRANDPA_TO_START[i].gameObject.SetActive(true);
            }
            Scr_GameTimer.EnableTimer();
            Scr_GameTimer.SetTimer();
            Destroy(TutoText);
            Destroy(this); 
        }
        if (tutoCompleted == false)
        {
            if (CAN_THE_PLAYER_MOVE_ON_TUTO == false)
            {
                gameObject.GetComponent<PlayerMove>().enabled = false;
                gameObject.GetComponent<PlayerLook>().enabled = false;
            }

            TutoText.GetComponent<TextMeshProUGUI>().enabled = true;

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

            TutoText.GetComponent<TextMeshProUGUI>().enabled = false;
            TutoText = GameObject.Find("TABLET");
            TutoText.GetComponent<TextMeshProUGUI>().enabled = true;

            GameObject TabletGO = scriptTablet.gameObject;
            TabletGO.GetComponent<TabletAction>().enabled = true;

            playerActionService.TUTOCanUseTablet = true;

            if (player.GetButton("Slap"))
            {
                if (timerForCamera >= 0.25f)
                {
                    didHeCamera = true;
                    timerForCamera = 0;
                }
                timerForCamera += Time.deltaTime;
            }
            if (player.GetButtonUp("Slap"))
            {
                timerForCamera = 0; 
            }
        }
        
        if (didHeCamera == true)
        {
            didHeSlap = false;
            gameObject.GetComponent<PlayerMove>().enabled = true;
            gameObject.GetComponent<PlayerLook>().enabled = true;

            TutoText.GetComponent<TextMeshProUGUI>().enabled = false;
            TutoText = GameObject.Find("RULES");
            TutoText.GetComponent<TextMeshProUGUI>().enabled = true;

            timerForTextTuto += Time.deltaTime;
            if (timerForTextTuto > 7)
            {
                didHeCamera = false;

                TutoText.GetComponent<TextMeshProUGUI>().enabled = false;
                TutoText = GameObject.Find("START");
                TutoText.GetComponent<TextMeshProUGUI>().enabled = true;

                DoorToOpen.GetComponent<Scr_FireDoor>().CAN_BE_CLOSED = true;
            }

        }

        if (DoorToOpen.GetComponent<Scr_FireDoor>().isClosed == false)
        {
            tutoCompleted = true;
        }
    }
}