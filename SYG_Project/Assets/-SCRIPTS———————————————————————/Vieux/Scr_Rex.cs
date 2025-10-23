using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Rex : Scr_Character
{

    private void Update()
    {   
        if(canMove)
        {
            SetDestination();
        }
        
    }

   
}
