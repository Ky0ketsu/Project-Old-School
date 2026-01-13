using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Scr_Rex : GrandpaParent
{
    [SerializeField]
    int life;

    float timerAfterHit;



    public List<AudioClip> inflateList;
    public List<AudioClip> deflateList;
    public List<AudioClip> cryList;
    [SerializeField] AudioSource audioPshht;

    void Start()
    {
        life = 3;
    }

    public override void Slap()
    {
        DeacreasedHp();
        Debug.Log(life);
    }

    void DeacreasedHp()
    {
        if(life > 0)
        {
        life--; 
        int r = Random.Range(0, deflateList.Count);
        audioPshht.clip = deflateList[r];
        audioPshht.Play();
        }
        
        timerAfterHit = 5f;
        if(life == 0 )
        {
            controlledMove = true;
            agent.SetDestination(bedroom.position);
        }

        if (life < 0 && !isStun)
        {
            Stun();

            int r = Random.Range(0, cryList.Count);
            audioPshht.clip = cryList[r];
            audioPshht.Play();
        }


    }

    private bool isStun;
    void Stun()
    {
        isStun = true;
        StartCoroutine(StunRoutine());
        canMove = false;
    }

    IEnumerator StunRoutine()
    {
        yield return new WaitForSeconds(4f);
        isStun = false;
        canMove = true;
    }

    protected override void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;
        if (inBedroom == true)
        {
            UpdateTimer();
        }
        else if (ArrivedToDestination())
        {
            if (controlledMove)
            {
                exitBedroomTimer = 10f;
                inBedroom = true;
                ActivateAgent(false);
            }
            else SetRandomDestination(transform.position, 10f);
        }

        if (agent.isActiveAndEnabled) agent.isStopped = !canMove;


        if (!isStun)
        {
            if (life < 3)
            {
                if (timerAfterHit > 0) timerAfterHit -= Time.deltaTime;
                else { life++; Debug.Log(life); }
            }
        }

        if(life > 0) controlledMove = false;
    }


}
