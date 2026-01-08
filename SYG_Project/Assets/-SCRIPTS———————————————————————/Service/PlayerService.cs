using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour, IPlayerService
{
    void Awake()
    {
        ServicesLocator.Register<IPlayerService>(this);
    }

    void OnDestroy()
    {
        ServicesLocator.Unregister<IPlayerService>();
    }

    [HideInInspector]
    public GameObject player;

    public void StunPlayer()
    {
        
    }

    public void SetPlayer(GameObject player)
    {
        player = this.player;
    }

}
