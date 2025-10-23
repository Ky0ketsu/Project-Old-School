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

    protected bool controlledMove;

    [SerializeField]private bool inBedroom;
    [SerializeField] private float timer;

    private void Awake()
    {
        EVENTS.OnGameplay += EnableMove;
        EVENTS.OnGameplayExit += DisableMove;
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
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }

    public virtual void SetDestination()
    {
        if (new Vector3(targetPosition.x, 0, targetPosition.z) == new Vector3(transform.position.x ,0 ,transform.position.z))
        {
            targetPosition += new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }
        agent.SetDestination(targetPosition);
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
        agent.SetDestination(bedroom.position);
        if (Vector3.Distance(transform.position, bedroom.position) < 3f)
        {
            canMove = false;
            agent.enabled = false;
            rigid.useGravity = false;
            transform.position += Vector3.down * 10;

            timer = 20f;
            inBedroom = true;
        }
    }

    void Update()
    {
        if (inBedroom == true)
        {
            Timer();
            Debug.Log("Time");
        }

        if (canMove && controlledMove)
        {
            GoBedroom();
        }
        if (canMove && controlledMove!)
        {
            SetDestination();
        }
    }

    void Timer()
    {
        timer -= 1 * Time.deltaTime;

        if(timer <= 0)
        {
            rigid.useGravity = true;
            transform.position += Vector3.up * 10;
            agent.enabled = true;
            canMove = true;
            inBedroom = false;
        }
    }
}
