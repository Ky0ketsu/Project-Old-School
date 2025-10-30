using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scr_TabletMove : MonoBehaviour
{
    public Vector3 initTransform;
    public Transform selfTransform; 

    

    public List<AudioClip> cameraActionOpenList;
    public List<AudioClip> cameraActionCloseList;
    [SerializeField] AudioSource audioCameraAction;
    public GameObject prefab;


    private void Start()
    {
        initTransform = transform.position;
        
        //selfTransform.localPosition = initTransform;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int r = Random.Range(0, cameraActionOpenList.Count);
            audioCameraAction.clip = cameraActionOpenList[r];
            audioCameraAction.Play();
        }


        if (Input.GetKey(KeyCode.E))
        {
            selfTransform.DOLocalMove(new Vector3(transform.position.x, 0, transform.position.z), 0.5f, false);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            selfTransform.DOLocalMove(new Vector3(0, transform.position.y - 1000, 0), 0.5f, false);

            int r = Random.Range(0, cameraActionCloseList.Count);
            audioCameraAction.clip = cameraActionCloseList[r];
            audioCameraAction.Play();
        }
    }
}
