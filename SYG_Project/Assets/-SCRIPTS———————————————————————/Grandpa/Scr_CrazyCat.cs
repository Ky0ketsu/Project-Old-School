using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrazyCat : Scr_GranpaOrigin
{
    [SerializeField] bool shearchPlayer;
    [SerializeField] LayerMask layerMask;

    [SerializeField]
    private float timerNotShowPlayer;


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
}
