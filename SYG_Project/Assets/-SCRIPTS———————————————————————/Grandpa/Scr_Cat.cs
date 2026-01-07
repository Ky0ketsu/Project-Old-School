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
    private bool canTimer = false; 

    [SerializeField]
    private float timerForFX = 111111110; 

    public void Awake()
    {
        
    }

    public void Slap()
    {
        crazyCat.GoBedroom();
        crazyCat.GetComponent<Scr_CrazyCat>().catIsSpawned = false;
        Destroy(gameObject);
    }

    public void Update()
    {
        timerForFX += Time.deltaTime;

        if (timerForFX >= 15)
        {
            timerForFX = 0;

            SpawnedFX = Instantiate(SpawnFX, transform); 
        }
    }
}
