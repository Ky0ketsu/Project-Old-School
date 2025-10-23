using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Character : MonoBehaviour , ISlapable
{
    [SerializeField] protected bool canMove;
    [SerializeField] protected Vector3 targetPosition;
    [HideInInspector] protected NavMeshAgent agent;

    [SerializeField]
    public Transform player;

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
    }

    public void Slap()
    {
        Slaped();
    }
}
