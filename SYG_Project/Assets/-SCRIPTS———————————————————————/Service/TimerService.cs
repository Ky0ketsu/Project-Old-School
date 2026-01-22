using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerService : MonoBehaviour
{
    void Awake()
    {
        ServicesLocator.Register<TimerService>(this);
    }

    void OnDestroy()
    {
        ServicesLocator.Unregister<TimerService>();    
    }

    [SerializeField]
    public GameObject timerObject;

    private void Start()
    {
        timerObject = gameObject;
    }


}
