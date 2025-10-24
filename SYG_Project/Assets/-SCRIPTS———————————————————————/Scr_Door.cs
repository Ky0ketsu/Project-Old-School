using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Door : MonoBehaviour, ISlapable
{
    public bool slaped;

    public void Slap()
    {
        slaped = true;
    }
}
