using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Speeder : Scr_Character
{
    [Range(0f, 50f)] float speed;

    public override void SetRandomDestination(Vector3 center, float randomMaxDistance)
    {
        base.SetRandomDestination(center, randomMaxDistance);
    }
}
