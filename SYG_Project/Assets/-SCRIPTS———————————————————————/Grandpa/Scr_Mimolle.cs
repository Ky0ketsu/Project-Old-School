using DG.Tweening;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scr_Mimolle : GrandpaParent
{

    [SerializeField] GameObject healMimolleVFX;

    [SerializeField] bool shearchPlayer;
    [SerializeField] LayerMask layerMask;

    [SerializeField]
    private float _timerNotShowPlayer;

    private float _timerAfterkickPlayer;


    public List<AudioClip> punchList;
    [SerializeField] AudioSource audioPunch;


    void CheckCanViewPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, Vector3.Distance(transform.position, player.position) * 1.05f, layerMask))
        {
            if (hit.transform.GetComponent<SlapAction>() != null && _timerAfterkickPlayer == 0)
            {
                if (!CanSeePlayer()) return;

                shearchPlayer = false;
                _timerNotShowPlayer = 3f;
                SetDestinationToPlayer();

                Debug.DrawLine(transform.position + Vector3.up, player.position, Color.green);
            }
            else
            {
                if (_timerNotShowPlayer > 0) _timerNotShowPlayer -= Time.deltaTime;
                if (_timerNotShowPlayer <= 0) _timerNotShowPlayer = 0f; shearchPlayer = true;

                Debug.DrawLine(transform.position + Vector3.up, player.position, Color.red);
            }
        }
    }

    void SetDestinationToPlayer()
    { 
        if (controlledMove == false)
        {
            targetPosition = player.position;
            SetRandomDestination(targetPosition, 0f);
        }
    }

    void CheckCanHitPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= 2f && _timerAfterkickPlayer == 0)
        {
            AttackPlayer();
        }
    }

    public override void Slap()
    {
        if (CanSeePlayer() == false)
        {
            Debug.Log($"{transform.name} a pris une claque");
            GoBedroom();
        }
    }

    public override void GoBedroom()
    {
        SetRandomDestination(bedroom.position,0);
        controlledMove = true;
        Debug.DrawLine(transform.position, targetPosition, Color.magenta, 10f);

        Instantiate(healMimolleVFX, transform.position, Quaternion.identity);
    }

    protected override void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;

        if (!controlledMove)
        {
            if (!CanSeePlayer()) spriteCanSlap = true;
            else spriteCanSlap = false;
        }
        else spriteCanSlap = false ;


        if (!shearchPlayer && !controlledMove) CheckCanHitPlayer();
        CheckCanViewPlayer();

        if (timerCanAttack > 0) timerCanAttack -= Time.deltaTime;
        else canAttack = true;

        if (_timerAfterkickPlayer > 0) _timerAfterkickPlayer -= Time.deltaTime;
        else _timerAfterkickPlayer = 0;


        if (inBedroom == true)
        {
            UpdateTimer();
        }
        else if (ArrivedToDestination())
        {
            if (controlledMove)
            {
                exitBedroomTimer = 100f;
                inBedroom = true;
                ActivateAgent(false);
            }
            else if(shearchPlayer) SetRandomDestination(transform.position, 10f);
        }

        if (agent.isActiveAndEnabled) agent.isStopped = !canMove;
    }



    bool canAttack;
    float timerCanAttack;

    void AttackPlayer()
    {
        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackAnimation());
            Invoke("CheckTouchPlayer", 0.6f);
            timerCanAttack = 2f;

        }

        
    }

    void PunchSound()
    {
        int r = Random.Range(0, punchList.Count);
        audioPunch.clip = punchList[r];
        audioPunch.Play();
    }

    void CheckTouchPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, 2.3f, layerMask))
        {

            if (hit.transform.GetComponentInParent<PlayerStunAction>() != null)
            {
                hit.transform.GetComponentInParent<PlayerStunAction>().Stun();
                StartCoroutine(AttackRoutine());
                _timerAfterkickPlayer = 5f;
            }
            else Debug.Log("mimolle na pas toucher");
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        agent.speed = 0;
        GetComponentInChildren<Scr_GrandpaSpriteDirection>().isAttack = true;
        yield return new WaitForSeconds(1f);
        agent.speed = 2;
        GetComponentInChildren<Scr_GrandpaSpriteDirection>().isAttack = false;
    }


    IEnumerator AttackAnimation()
    {
        transform.GetChild(0).DOScaleY(0.7f, 0.45f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).DOScaleY(1f, 0.2f).SetEase(Ease.OutCubic);
        PunchSound();
       
    }
}
