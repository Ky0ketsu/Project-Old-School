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

    [SerializeField]
    private GameObject _player;

    public void StunPlayer()
    {

    }

    public void EnableMovePlayer()
    {

    }

    public void DisableMovePlayer()
    {

    }
    
}
