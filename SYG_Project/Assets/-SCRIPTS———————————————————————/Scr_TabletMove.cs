using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Rewired;
using UnityEngine;

public class Scr_TabletMove : MonoBehaviour
{
    public Vector3 initTransform;
    public Transform selfTransform; 

    

    public List<AudioClip> cameraActionOpenList;
    public List<AudioClip> cameraActionCloseList;
    [SerializeField] AudioSource audioCameraAction;
    public GameObject prefab ;
    [SerializeField] float timer = 0;

    Player player;


    private void Start()
    {
        initTransform = transform.position;

        player = ReInput.players.GetPlayer(0);

        //selfTransform.localPosition = initTransform;
    }
    public void Update()
    {




        if (player.GetButton("Slap"))
        {
            if (timer >= 0.25f)
            {
                int r = Random.Range(0, cameraActionOpenList.Count);
                audioCameraAction.clip = cameraActionOpenList[r];
                audioCameraAction.Play();


                timer = 0;

                selfTransform.DOLocalMove(new Vector3(transform.position.x, 0, transform.position.z), 0.5f, false);

            }
            timer += Time.deltaTime;
        }

        if (player.GetButtonUp("Slap"))
        {
            selfTransform.DOLocalMove(new Vector3(0, transform.position.y - 1000, 0), 0.5f, false);

            int r = Random.Range(0, cameraActionCloseList.Count);
            audioCameraAction.clip = cameraActionCloseList[r];
            audioCameraAction.Play();

            timer = 0;

        }


       
    }


}
