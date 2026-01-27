using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_HealSFX : MonoBehaviour
{

    public List<AudioClip> healList;
    [SerializeField] AudioSource audioHeal;



    // Start is called before the first frame update
    void Start()
    {
        int d = Random.Range(0, healList.Count);
        audioHeal.clip = healList[d];
        audioHeal.Play();
    }
}
