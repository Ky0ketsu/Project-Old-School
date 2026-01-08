using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Scr_Mimolle : GrandpaParent
{
    [SerializeField] bool shearchPlayer;
    [SerializeField] LayerMask layerMask;

    [SerializeField]
    private float timerNotShowPlayer;

    private float timerAfterkickPlayer;

    void CheckCanViewPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, Vector3.Distance(transform.position, player.position) * 1.05f, layerMask))
        {
            if (hit.transform.GetComponent<SlapAction>() != null && timerAfterkickPlayer == 0)
            {
                shearchPlayer = false;
                timerNotShowPlayer = 3f;

                Debug.DrawRay(transform.position + Vector3.up, (player.position - transform.position).normalized * Vector3.Distance(transform.position, player.position), Color.green);
            }
            else
            {
                if (timerNotShowPlayer > 0) timerNotShowPlayer -= Time.deltaTime;
                if (timerNotShowPlayer <= 0) timerNotShowPlayer = 0f; shearchPlayer = true;

                Debug.DrawRay(transform.position + Vector3.up, (player.position - transform.position).normalized * Vector3.Distance(transform.position, player.position), Color.red);
            }
        }
    }

    void SetDestinationToPlayer()
    {
        targetPosition = player.position;
    }

    void CheckCanHitPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= 2f && timerAfterkickPlayer == 0)
        {
            AttackPlayer();
        }
    }

    public override void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;

        if (!shearchPlayer) CheckCanHitPlayer();
        CheckCanViewPlayer();

        if (timerCanAttack > 0) timerCanAttack -= Time.deltaTime;
        else canAttack = true;


        if (timerAfterkickPlayer > 0) timerAfterkickPlayer -= Time.deltaTime;
        else timerAfterkickPlayer = 0;


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
            else if(shearchPlayer) SetRandomDestination(transform.position, 10f);
            else SetDestinationToPlayer();
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

    void CheckTouchPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, 2.3f, layerMask))
        {

            if (hit.transform.GetComponentInParent<PlayerStunAction>() != null)
            {
                hit.transform.GetComponentInParent<PlayerStunAction>().Stun();
                timerAfterkickPlayer = 15f;
            }
            else Debug.Log("mimolle na pas toucher");
        }
    }

    IEnumerator AttackAnimation()
    {
        transform.GetChild(0).DOScaleY(0.7f, 0.45f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).DOScaleY(1f, 0.2f).SetEase(Ease.OutCubic);
       
    }
}
