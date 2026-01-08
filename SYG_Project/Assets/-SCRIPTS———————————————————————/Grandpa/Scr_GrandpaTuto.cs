using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GrandpaTuto : GrandpaParent
{

    public override void Slap()
    {
        Debug.Log("tu as sauvé tuto");
        player.GetComponent<Scr_Tuto>().didHeSlap = true;
        player.GetComponent<Scr_Tuto>().timer = 0; 
    }

    private void Update()
    {
        if (player.GetComponent<Scr_Tuto>().tutoCompleted == true) Destroy(gameObject); 
    }

}
