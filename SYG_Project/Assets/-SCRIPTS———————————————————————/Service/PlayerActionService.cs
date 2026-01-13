using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionService : MonoBehaviour, IPlayerActionService
{
    Player player;

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

        if(_pressTime > 1f)
        {
            EnterTabletView();
        }

        if(player.GetButtonUp("Slap"))
        {
            if(_pressTime < 1f)
            {
                Slap();
            }
            else
            {
                ExitTabletView();
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
        if(tabletAction == null) return; 
        tabletAction.EnterTabletView();
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
