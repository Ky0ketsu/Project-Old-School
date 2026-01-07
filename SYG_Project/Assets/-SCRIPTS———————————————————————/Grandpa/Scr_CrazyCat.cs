using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrazyCat : GrandpaParent
{
    [SerializeField] bool shearchPlayer;
    [SerializeField] LayerMask layerMask;

    [SerializeField]
    private float timerNotShowPlayer;

    [SerializeField]
    bool catIsSlaped;

    [SerializeField]
    GameObject catPrefab;
    [SerializeField]
    public bool catIsSpawned = false;
    GameObject currentCat;
    [SerializeField]
    private Transform[] catSpawnPoint;

    protected override void Start()
    {
        base.Start();
        
    }

    void CheckCanViewPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, Vector3.Distance(transform.position, player.position) * 1.05f, layerMask))
        {
            if (hit.transform.GetComponent<Scr_Player_Slap>() != null)
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



    public override void Slap()
    {
        Debug.Log("Trouve le chat");
        if (catIsSpawned == false)
        {
            currentCat = Instantiate(catPrefab, catSpawnPoint[Random.Range(0, catSpawnPoint.Length)].position, Quaternion.identity);
            currentCat.GetComponent<Scr_Cat>().crazyCat = this;
            catIsSpawned = true;
        }
    }

    public override void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;

        CheckCanViewPlayer();


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
            else if (shearchPlayer) SetRandomDestination(transform.position, 10f);
            else SetDestinationToPlayer();
        }

        if (agent.isActiveAndEnabled) agent.isStopped = !canMove;
    }

    void SetDestinationToPlayer()
    {
        targetPosition = player.position;
    }
}
