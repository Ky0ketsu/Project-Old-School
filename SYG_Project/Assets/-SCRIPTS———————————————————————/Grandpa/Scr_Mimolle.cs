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
        if(Physics.Raycast(transform.position + Vector3.up * 1.5f, transform.position - player.position, out hit, Vector3.Distance(transform.position , player.position), layerMask))
        {
            Debug.DrawRay(transform.position + Vector3.up, (transform.position - player.position * 1.5f) * hit.distance, Color.green);
            shearchPlayer = true;
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up, (transform.position - player.position * 1.5f) * hit.distance, Color.red);
            StartCoroutine(DelayToShearch());
        }
    }

    IEnumerator DelayToShearch()
    {
        yield return new WaitForSeconds(2f);
        shearchPlayer = true;
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
        if (shearchPlayer! && player != null)
        {
            targetPosition = player.position;
        }

        agent.SetDestination(targetPosition);
    }


}
