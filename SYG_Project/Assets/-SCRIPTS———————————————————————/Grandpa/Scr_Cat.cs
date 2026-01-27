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
    
    [SerializeField]
    public List<AudioClip> meowList;
    [SerializeField] AudioSource audioCat;

    public bool spriteCanSlap;

    void Start()
    {
        spriteCanSlap = true;
    }

    public void Slap()
    {
        crazyCat.GoBedroom();
        spriteCanSlap = false;
        Destroy(gameObject);
        crazyCat._catIsSpawned = false;
    }

    public void Update()
    {
        timerForFX -= Time.deltaTime;

        if (timerForFX <= 0)
        {
            timerForFX = 15;

            SpawnedFX = Instantiate(SpawnFX, transform); 

            int r = Random.Range(0, meowList.Count);
            audioCat.clip = meowList[r];
            audioCat.Play();
        }
    }
}
