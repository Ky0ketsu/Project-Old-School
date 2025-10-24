using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Scr_Rex : Scr_Character
{
    [SerializeField]
    int life;
    float timerHp;
    float timerStun;
    bool Stuned;

    private void Start()
    {
        life = 3;
    }

    public override void Slaped()
    {
        DeacreasedHp();
        Debug.Log(life);
    }

    void DeacreasedHp()
    {
        if(life > 0)
        { life--; }
        
        timerHp = 5f;
        if(life == 0 )
        {
            controlledMove = true;
        }

        if (life < 0)
        {
            Stuned = true;
            canMove = false;
            timerStun = 10f;
        }
    }

    private void Update()
    {
        if(life < 3 && timerHp > 0)
        {
            timerHp -= Time.deltaTime;
        }
        if(life < 3 && timerHp <= 0)
        {
            life++;
            Debug.Log(life);
        }

        if(life > 0)
        {
            controlledMove = false;
        }


        if(Stuned == true && timerStun > 0)
        {
            timerStun -= Time.deltaTime;
        }
        if(Stuned == true && timerStun <= 0)
        {
            canMove = true;
            Stuned = false;
        }
    }


}
