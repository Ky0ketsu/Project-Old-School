using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Character : MonoBehaviour , ISlapable
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

    [SerializeField] private bool inBedroom;
    [SerializeField] private float timer;

    private void Awake()
    {
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

    private void Start()
    {
        targetPosition = transform.position;
    }

    //recherche une position aleatoire autour de lui même
    public virtual void SetRandomDestination(Vector3 center, float randomMaxDistance)
    {
        NavMeshHit hit;
        bool foundDestination = false;
        while (foundDestination==false)
        {
            Vector3 randomPoint = center + new Vector3(Random.Range(-randomMaxDistance, randomMaxDistance), 0, Random.Range(-randomMaxDistance, randomMaxDistance));
            //regarde si le point touche le nav mesh
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                foundDestination = true;
            }
        }

        agent.SetDestination(targetPosition);
    }

    //public void ReplaceOnMesh()
    //{
    //    NavMeshHit hit;
    //    if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
    //    {
    //        transform.position = hit.position;
    //        Debug.Log("Replace");
    //    }

      
    //}

    public virtual void Slaped()
    {
        Debug.Log($"{transform.name} a pris une claque");
        GoBedroom();
    }

    public void Slap()
    {
        Slaped();
    }

    public virtual void GoBedroom()
    {
        controlledMove = true;
        SetRandomDestination(bedroom.position,0);
    }

    void ArrivedInBedroom()
    {
        canMove = false;
        agent.enabled = false;
        transform.position += Vector3.down * 10;
        timer = 10f;
        inBedroom = true;
    }

    void FixedUpdate()
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
                timer = 10f;
                inBedroom = true;
                ActivateAgent(false);
            }
            else SetRandomDestination(transform.position, 10f);
        }

        if (agent.isActiveAndEnabled) agent.isStopped = !canMove;
    }

    bool ArrivedToDestination()
    {
        return (agent.destination - transform.position).magnitude < 2f;
    }

    // temps avant que le vieux resorte du sa chambre
    void UpdateTimer()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 5)
        {
            bedroom.GetComponent<Scr_Door>().halfOpen = true;
        }

        if (timer <= 0)
        {
            ActivateAgent(true);
        }

        if (bedroom.GetComponent<Scr_Door>().slaped == true)
        {
            timer = 10f;
            bedroom.GetComponent<Scr_Door>().CloseDoor();
            bedroom.GetComponent<Scr_Door>().slaped = false;
        }
    }

    void ActivateAgent(bool wanted)
    {
        if (wanted)
        {
            //transform.position += Vector3.up * 10f;
            graphics.SetActive(true);
            colliders.SetActive(true);
            agent.enabled = true;
            canMove = true;
            bedroom.GetComponent<Scr_Door>().CloseDoor();
            controlledMove = false;
            SetRandomDestination(transform.position, 10f);
            inBedroom = false;
        }
        else
        {
            //transform.position -= Vector3.up * 10f;
            graphics.SetActive(false);
            colliders.SetActive(false);
            agent.enabled = false;
            canMove = false;
            bedroom.GetComponent<Scr_Door>().CloseDoor();
        }
    }
}
