using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GrandpaParent : MonoBehaviour , ISlapable
{

    [SerializeField] 
    protected bool canMove;

    [SerializeField] GameObject graphics, colliders;

    protected Vector3 targetPosition;
    protected NavMeshAgent agent;

    [SerializeField]
    public Transform player;

    
    public Transform bedroom;

    [SerializeField]
    protected bool controlledMove;

    [SerializeField]
    public bool inBedroom;
    [SerializeField]
    protected float exitBedroomTimer;


    [SerializeField]
    protected GameObject leaveBedroomFX;

    private void Awake()
    {
        targetPosition = transform.position;
        EVENTS.OnGameplay += EnableMove;
        EVENTS.OnGameplayExit += DisableMove;
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnDestroy()
    {
        EVENTS.OnGameplay -= EnableMove;
        EVENTS.OnGameplayExit -= DisableMove;
    }

    void EnableMove()
    {
        canMove = true;
    }

    void DisableMove()
    {
        canMove = false;
    }


    public virtual void SetRandomDestination(Vector3 center, float randomMaxDistance)
    {
        NavMeshHit hit;
        bool foundDestination = false;
        while (foundDestination==false)
        {
            Vector3 randomPoint = center + new Vector3(Random.Range(-randomMaxDistance, randomMaxDistance), 0, Random.Range(-randomMaxDistance, randomMaxDistance));
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                foundDestination = true;
            }
        }

        agent.SetDestination(targetPosition);
    }


    public virtual void Slap()
    {
        Debug.Log($"{transform.name} a pris une claque");
        GoBedroom();
    }



    public virtual void GoBedroom()
    {
        SetRandomDestination(bedroom.position,0);
        controlledMove = true;
        Debug.DrawLine(transform.position, targetPosition, Color.magenta, 10f);
    }

    protected virtual void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;

        if (inBedroom == true)  UpdateTimer();

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
    }

    protected bool ArrivedToDestination()
    {
        return (agent.destination - transform.position).magnitude < 2f;
    }

    [HideInInspector]
    private Scr_GameTimer timer;

    protected virtual void Start()
    {
        timer = FindAnyObjectByType<Scr_GameTimer>();
    }

     protected void UpdateTimer()
    {
        if (timer.currentTimer <= 60) return;

        exitBedroomTimer -= Time.deltaTime;

        if (exitBedroomTimer <= 5)
        {
            bedroom.GetComponent<Scr_Door>().halfOpen = true;
        }

        if (exitBedroomTimer <= 0)
        {
            ActivateAgent(true);
        }

        if (bedroom.GetComponent<Scr_Door>().slaped == true)
        {
            exitBedroomTimer = 10f;
            bedroom.GetComponent<Scr_Door>().CloseDoor();
            bedroom.GetComponent<Scr_Door>().slaped = false;
        }
    }

    protected void ActivateAgent(bool wanted)
    {
        graphics.SetActive(wanted); colliders.SetActive(wanted);
        agent.enabled = wanted;
        canMove = wanted;


        if (wanted)
        {
            bedroom.GetComponent<Scr_Door>().CloseDoor();
            controlledMove = false;
            SetRandomDestination(transform.position, 10f);
            inBedroom = false;


            if (leaveBedroomFX != null) ServicesLocator.Get<IFXService>().PlayFx(leaveBedroomFX, transform.position);
            else Debug.Log("Pas de FX");
        }
        else
        {
            bedroom.GetComponent<Scr_Door>().CloseDoor();
        }
    }

    private float _viewAngle = 60f;
    [SerializeField]
    private LayerMask _obstacleMask;

    protected bool CanSeePlayer()
    { 
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToPlayer) < _viewAngle / 2f)
        {
            float dist = Vector3.Distance(transform.position, player.position);
            if (!Physics.Raycast(transform.position, dirToPlayer, dist, _obstacleMask))
            {
                Debug.DrawLine(transform.position, player.position, Color.green, 0.5f);
                return true;
            }
        }
        return false;
    }
}
