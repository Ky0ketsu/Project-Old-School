using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Character : MonoBehaviour , ISlapable
{
    private Rigidbody rigid;

    [SerializeField] 
    protected bool canMove;

    protected Vector3 targetPosition;
    protected NavMeshAgent agent;

    [HideInInspector]
    public Transform player;

    [HideInInspector]
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
        rigid = GetComponent<Rigidbody>();
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

    public virtual void SetDestination()
    {
        NavMeshPath navPath = new NavMeshPath();
        if(agent.isOnNavMesh && new Vector3(targetPosition.x, 0, targetPosition.z) == new Vector3(transform.position.x, 0, transform.position.z) || agent.CalculatePath(new Vector3(targetPosition.x, 0, targetPosition.z), navPath) == false)
        {
            Vector3 randomPoint = transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            NavMeshHit hit;

            //regarde si le point touche le nav mesh
            if(NavMesh.SamplePosition(randomPoint, out hit, 10f , NavMesh.AllAreas))
            {
                targetPosition = hit.position;
            }
        }
    }

    public virtual void Slaped()
    {
        Debug.Log($"{transform.name} a pris une claque");
        controlledMove = true;
    }

    public void Slap()
    {
        Slaped();
    }

    public virtual void GoBedroom()
    {
        targetPosition = bedroom.position;
        if(agent.isOnNavMesh)
        {
            if (Vector3.Distance(transform.position, bedroom.position) < 3f)
            {
                canMove = false;
                agent.enabled = false;
                rigid.useGravity = false;
                transform.position += Vector3.down * 10;

                timer = 10f;
                inBedroom = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (inBedroom == true)
        {
            Timer();
        }

        //Deffinit le type de deplacement
        if (controlledMove)
        {
            GoBedroom();
        }
        else
        {
            SetDestination();
        }

        if (canMove) agent.SetDestination(targetPosition);

        if (!agent.isOnNavMesh) Debug.LogWarning($"{transform.name} n'est pas sur le navmesh");
    }

    // temps avant que le vieux resorte du sa chambre
    void Timer()
    {
        timer -= Time.deltaTime;

        if(bedroom.GetComponent<Scr_Door>().slaped == true)
        {
            timer = 10f;
            bedroom.GetComponent<Scr_Door>().slaped = false;
        }

        if(timer <= 0)
        {
            rigid.useGravity = true;
            transform.position += Vector3.up * 11f;
            agent.enabled = true;
            canMove = true;
            inBedroom = false;
            controlledMove = false;
        }
    }
}
