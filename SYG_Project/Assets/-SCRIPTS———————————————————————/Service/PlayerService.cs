using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour, IPlayerService
{
    void Awake()
    {
        ServicesLocator.Register<PlayerService>(this);
    }

    void OnDestroy()
    {
        ServicesLocator.Unregister<PlayerService>();
    }

    [SerializeField]
    public GameObject player;

    public void StunPlayer()
    {
        
    }

    public void SetPlayer(GameObject player)
    {
        player = this.player;
    }

}
