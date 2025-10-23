using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scr_Mimolle : Scr_Character
{
    [SerializeField] bool shearchPlayer;
    [SerializeField] LayerMask layerMask;


    private void Update()
    {
        CheckCanViewPlayer();
    }

    void CheckCanViewPlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up * 1.5f, transform.position - player.position ), out hit, Vector3.Distance(transform.position , player.position) )
    }

    public override void SetDestination()
    {   
        if (new Vector3(targetPosition.x, 0, targetPosition.z) == new Vector3(transform.position.x, 0, transform.position.z) && shearchPlayer)
        {
            if (player != null)
            {
                targetPosition = player.position + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
            }
        }
        agent.SetDestination(targetPosition);
    }


}
