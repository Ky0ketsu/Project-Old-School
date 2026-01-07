using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Cat : MonoBehaviour , ISlapable
{
    [SerializeField]
    public Scr_CrazyCat crazyCat;

    [SerializeField]
    private GameObject SlapFX;

    public void Slap()
    {
        crazyCat.GoBedroom();
        crazyCat.GetComponent<Scr_CrazyCat>().catIsSpawned = false;
        Destroy(gameObject);
    }
}
