using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Rex : GrandpaParent
{

    [SerializeField] GameObject healRexVFX;

    [SerializeField]
    int life;


    public List<AudioClip> inflateList;
    public List<AudioClip> deflateList;
    public List<AudioClip> cryList;
    [SerializeField] AudioSource audioPshht;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _speed, _escapeSpeed;

    void Start()
    {
        life = 3;
    }

    public override void Slap()
    {
        DeacreasedHp();
        Debug.Log(life);
    }

    IEnumerator EscapeRoutine()
    {
        _animator.speed = _escapeSpeed;
        agent.speed = 5f;
        yield return new WaitForSeconds(3f);
        _animator.speed = _speed;
        agent.speed = 2f;
    }

    void DeacreasedHp()
    {
        if(life > 0)
        {
        life--; 
        int r = Random.Range(0, deflateList.Count);
        audioPshht.clip = deflateList[r];
        audioPshht.Play();
        SetRandomDestination(transform.position, 200f);
        
        StartCoroutine(EscapeRoutine());
        }
        
        if(life == 0 )
        {
            controlledMove = true;
            agent.SetDestination(bedroom.position);
        }

        if (life <= 0 && !isStun)
        {
            StartCoroutine(StunRoutine());

            Instantiate(healRexVFX, transform.position, Quaternion.identity);
        }
    }

    private bool isStun;
    IEnumerator StunRoutine()
    {
        Debug.Log("Rex CRY");
        isStun = true;
        canMove = false;

        yield return new WaitForSeconds(4f);

        isStun = false;
        canMove = true;
    }

    protected override void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;

        if (!inBedroom && !ArrivedToDestination() && !isStun)
        {
            int d = Random.Range(0, footStep.Count);
            audioFootStep.clip = footStep[d];
            audioFootStep.Play();
        }

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
                life = 3;
            }
            else SetRandomDestination(transform.position, 10f);
        }

        if (agent.isActiveAndEnabled) agent.isStopped = !canMove;

        if(life > 0) controlledMove = false;
    }


}
