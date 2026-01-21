using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GrandpaTuto : GrandpaParent
{
    public override void Slap()
    {
        Debug.Log("tu as sauvé tuto");
        player.GetComponent<Scr_Tuto>().didHeSlap = true;
        player.GetComponent<Scr_Tuto>().timerForCamera = 0; 
        SetRandomDestination(bedroom.transform.position, 0);
    }

    protected override void Update()
    {
        if (player.GetComponent<Scr_Tuto>().tutoCompleted == true) Destroy(gameObject); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigeeer");
        if (bedroom.GetComponent<BoxCollider>() == other)
        {
            Debug.Log("a pu tuto");
            Destroy(gameObject);
        }
    }

}
