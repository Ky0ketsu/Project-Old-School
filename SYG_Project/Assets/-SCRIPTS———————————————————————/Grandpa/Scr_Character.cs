using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Character : MonoBehaviour , ISlapable
{
    private Rigidbody rigid;

    [SerializeField] protected bool canMove;
    [SerializeField] protected Vector3 targetPosition;
    [HideInInspector] protected NavMeshAgent agent;

    [SerializeField]
    public Transform player;

    [SerializeField] public Transform bedroom;

    [SerializeField] protected bool controlledMove;

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
        if(agent.isOnNavMesh)
        {
            if (new Vector3(targetPosition.x, 0, targetPosition.z) == new Vector3(transform.position.x ,0 ,transform.position.z))
            {
                targetPosition += new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            }
            agent.SetDestination(targetPosition);
            }
        else
        {
            Debug.Log("Agent is not on navmesh");
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
        if(agent.isOnNavMesh)
        {
            agent.SetDestination(bedroom.position);
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
        else
        {
            Debug.Log("Agent is not on navmesh");
        }
    }

    void FixedUpdate()
    {
        if (inBedroom == true)
        {
            Timer();
        }

        if (canMove && controlledMove)
        {
            GoBedroom();
        }
        else if (canMove && !controlledMove)
        {
            SetDestination();
        }
    }

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
