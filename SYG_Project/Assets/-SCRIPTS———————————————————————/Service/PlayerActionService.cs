using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionService : MonoBehaviour, IPlayerActionService
{
    Player player;

    public bool TUTOCanUseTablet = true; 

    private void Awake()
    {
        ServicesLocator.Register<IPlayerActionService>(this);
        player = ReInput.players.GetPlayer(0);
    }

    void OnDestroy()
    {
        ServicesLocator.Unregister<IPlayerActionService>();
    }

    [HideInInspector]
    private bool tabletOpen = false;
    public SlapAction slapAction;
    [HideInInspector]
    public TabletAction tabletAction;

    private float _pressTime;

    void Update()
    {
        if(player.GetButton("Slap"))
        {
            _pressTime += Time.deltaTime;
        }

        if(_pressTime > 0.7f && tabletOpen == false)
        {
            
            tabletOpen = true;
            EnterTabletView();
        }

        if(player.GetButtonUp("Slap"))
        {
            if(_pressTime < 0.7f)
            {
                Slap();
            }
            else
            {
                if(tabletOpen == true)
                {
                    tabletOpen = false;
                    ExitTabletView();
                }
            }
            _pressTime = 0;
        }


    }

    public void Slap()
    {
        if (slapAction == null) return;
        slapAction.SlapWanted();
    }
    public void EnterTabletView()
    {
        if (tabletAction == null)
        {
            Debug.LogWarning("Pas de tablet");
            return;
        }
        if(TUTOCanUseTablet == true) tabletAction.EnterTabletView();
    }
    public void ExitTabletView()
    {
        if (tabletAction == null) return;
        tabletAction.ExitTabletView();
    }

    public void SetSlapAction(SlapAction slap)
    {
        slapAction = slap;
    }

    public void SetTabletAction(TabletAction tablet)
    {
        tabletAction = tablet;
    }

}
