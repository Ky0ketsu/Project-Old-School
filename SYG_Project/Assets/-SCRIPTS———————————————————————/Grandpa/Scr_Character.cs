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

    [SerializeField] private Scr_Door personalDoor; 

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
    public virtual void SetDestination()
    {
        if(agent.isOnNavMesh &&  Vector3.Distance(new Vector3(targetPosition.x, 0, targetPosition.z), new Vector3(transform.position.x, 0, transform.position.z)) < 2f)
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
            if (Vector3.Distance(transform.position, bedroom.position) < 2f)
            {
                canMove = false;
                agent.enabled = false;
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
    }

    // temps avant que le vieux resorte du sa chambre
    void Timer()
    {
        timer -= Time.deltaTime;

        if (timer <= 5)
        {
            bedroom.GetComponent<Scr_Door>().halfOpen = true;
        }

        if (timer <= 0)
        {
            transform.position += Vector3.up * 10f;
            agent.enabled = true;
            canMove = true;
            inBedroom = false;
            controlledMove = false;
            bedroom.GetComponent<Scr_Door>().CloseDoor();

        }

        if (bedroom.GetComponent<Scr_Door>().slaped == true)
        {
            timer = 10f;
            bedroom.GetComponent<Scr_Door>().CloseDoor();
            bedroom.GetComponent<Scr_Door>().slaped = false;
        }
    }
}
