using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Amb_Light : MonoBehaviour
{
    public List<AudioClip> buzzList;
    public AudioSource buzz;

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, buzzList.Count);
        AudioClip clip = buzzList[r];
        buzz.clip = clip;
        buzz.Play();

    }

    // Update is called once per frame
    void Update()
    {
            
        
    }
}
