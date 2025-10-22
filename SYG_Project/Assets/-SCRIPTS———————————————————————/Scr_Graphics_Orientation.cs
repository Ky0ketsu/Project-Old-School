using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Graphics_Orientation : MonoBehaviour
{
    [SerializeField] Transform look;

    void Update()
    {
        transform.eulerAngles = look.eulerAngles ;
    }
}
