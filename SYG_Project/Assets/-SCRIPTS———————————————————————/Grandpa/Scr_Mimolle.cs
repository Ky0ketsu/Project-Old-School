using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Mimolle : Scr_Character
{
    [SerializeField] bool shearchPlayer;
    [SerializeField] LayerMask layerMask;

    //temps depuis le lequel il n'a pas vue le joueur
    [SerializeField]
    private float timerNotShowPlayer;


    private void Update()
    {   
        CheckCanViewPlayer();
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
                timerNotShowPlayer -= Time.deltaTime;
                if (timerNotShowPlayer <= 0) shearchPlayer = true;
                Debug.DrawRay(transform.position + Vector3.up, (player.position - transform.position).normalized * Vector3.Distance(transform.position, player.position), Color.red);
            }


        }
    }

    public override void SetDestination()
    {
        if (shearchPlayer)
        {
            if (agent.isOnNavMesh && new Vector3(targetPosition.x, 0, targetPosition.z) == new Vector3(transform.position.x, 0, transform.position.z))
            {
                Vector3 randomPoint = player.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
                {
                    targetPosition = hit.position;
                    agent.SetDestination(targetPosition);
                }
            }
        }
        else
        {
            targetPosition = player.position;
        }

        /*if (new Vector3(targetPosition.x, 0, targetPosition.z) == new Vector3(transform.position.x, 0, transform.position.z) && shearchPlayer)
        {
            if (player != null)
            {
                targetPosition = player.position + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
            }
        }
        if (shearchPlayer! && player != null)
        {
            targetPosition = player.position;
        }*/

        agent.SetDestination(targetPosition);
    }


}
