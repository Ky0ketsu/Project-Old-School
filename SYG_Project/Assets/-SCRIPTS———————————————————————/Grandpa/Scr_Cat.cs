using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Cat : MonoBehaviour , ISlapable
{
    [SerializeField]
    public Scr_CrazyCat crazyCat;

    [SerializeField]
    private GameObject SlapFX;

    [SerializeField]
    private GameObject SpawnFX;
    private GameObject SpawnedFX;

    [SerializeField]
    private float timerForFX = 0; 

    public void Awake()
    {
        
    }

    public void Slap()
    {
        crazyCat.GoBedroom();
        Destroy(gameObject);
    }

    public void Update()
    {
        timerForFX -= Time.deltaTime;

        if (timerForFX <= 0)
        {
            timerForFX = 15;

            SpawnedFX = Instantiate(SpawnFX, transform); 
        }
    }
}
